using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Application.Interfaces.Repositories;
using GienPhim_Services.UserService.Application.Interfaces.Validators;
using GienPhim_Services.UserService.Domain.Entities;
using GienPhim_Services.UserService.Domain.Exceptions.Duplicate;
using GienPhim_Services.UserService.Domain.Exceptions.Failed;
using GienPhim_Services.UserService.Domain.Exceptions.Invalid;
using GienPhim_Services.UserService.Domain.Exceptions.NotFounds;
using GienPhim_Services.UserService.Domain.Factories.Interfaces;
using GienPhim_Services.UserService.Domain.Services.Interfaces;
using GienPhim_Services.UserService.Infrastructure.Config;
using GienPhim_Services.UserService.Shared.Kernel;
using Microsoft.Extensions.Options;
using Shared.FileStorage;

namespace GienPhim_Services.UserService.Domain.Services
{
    public class UserDomainService : IUserDomainService
    { 
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidationService _validationService;
        private readonly IGuidUserNameGenerator _guidUserNameGenerator;
        private readonly IUserFactory _userFactory; 
        private readonly string _defaultRole;
        private readonly ILogger<UserDomainService> _logger;

        public UserDomainService(IUnitOfWork unitOfWork, IValidationService validationService
            , IGuidUserNameGenerator guidUserNameGenerator, IUserFactory userFactory
            , IOptions<UserSettings> userSettings, ILogger<UserDomainService> logger)
        {
            _unitOfWork = unitOfWork;
            _validationService = validationService;
            _guidUserNameGenerator = guidUserNameGenerator;
            _userFactory = userFactory;
            _defaultRole = userSettings.Value.DefaultRole ?? "User";
            _logger = logger;
        }

        // Đăng ký tài khoản Local
        public async Task<User> RegisterUserAsync(User user, string password, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DomainSerVice: Bắt đầu đăng ký user {Email}", user.Email);
            // await using → gọi DisposeAsync() (async).
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Tạo User mới
            var result = await _unitOfWork.Users.RegisterUserAsync(user, password);
            if (!result.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogWarning("DomainSerVice: Đăng ký user {Email} thất bại!", user.Email);
                throw new CreateUserFailedException(user.Email);
            }

            // Gán role cho User mới tạo
            var resultAssign = await _unitOfWork.Users.AssignRoleToUserAsync(user, _defaultRole);
            if (!resultAssign.Succeeded)
            {
                _logger.LogError("DomainSerVice: Gán vai trò cho user {Email} thất bại!", user.Email);
                await transaction.RollbackAsync(cancellationToken);
                throw new AssignRoleFailedException(user.Email,_defaultRole);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("DomainSerVice: Đăng ký user {Email} thành công!", user.Email);
            return user;
        }

        // Login hoặc đăng ký tài khoản từ bên ngoài
        public async Task<User> ProviderLoginOrRegisterUserAsync(ExternalLoginUserInfoDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DomainSerVice: Bắt đầu đăng nhập hoặc đăng ký user {Email} bên thứ 3", request.Email);

            // Kiểm tra user đã tồn tại chưa
            var existingUser = await _unitOfWork.Users.FindByAuthIdAndTypeLoginAsync(request.AuthId, request.TypeLogin, cancellationToken);
            if (existingUser == null)
            {
                _logger.LogInformation("DomainSerVice: User {Email} chưa tồn tại, bắt đầu đăng ký mới", request.Email);   
                
                await _validationService.ValidateAsync(request, cancellationToken);
                // Chưa có user thì đăng ký mới
                var user = await ProviderRegisterUserAsync(request, cancellationToken);

                return await ProviderLoginUserAsync(user, cancellationToken);
            }
            else
            {
                // Đã có user thì login
                _logger.LogInformation("DomainSerVice: User {Email} đã tồn tại, bắt đầu đăng nhập", request.Email);
                return await ProviderLoginUserAsync(existingUser, cancellationToken);
            }
        }

        // Login tài khoản từ bên ngoài nếu email chưa có đăng ký
        public async Task<User> ProviderRegisterUserAsync(ExternalLoginUserInfoDTO request ,CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DomainSerVice: Bắt đầu đăng ký user {Email} bên thứ 3", request.Email);

            // await using → gọi DisposeAsync() (async).
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Tạo user mới
            var userName = _guidUserNameGenerator.GenerateUserName();
            _logger.LogInformation("DomainSerVice: Username ngẫu nhiên tạo cho user {Email} là {UserName}", request.Email, userName);

            // Tạo đối tượng User mới
            var user = _userFactory.CreateExternalUser(request, userName);

            // Đăng ký User mới
            var result = await _unitOfWork.Users.RegisterUserNotPasswordAsync(user);
            if (!result.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogWarning("DomainSerVice: Đăng ký user {Email} bên thứ 3 thất bại!", request.Email);
                throw new CreateUserFailedException(request.Email);
            }

            // Gán role cho User mới tạo
            var resultAssign = await _unitOfWork.Users.AssignRoleToUserAsync(user, _defaultRole);
            if (!resultAssign.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError("DomainSerVice: Gán vai trò cho user {Email} bên thứ 3 thất bại!", request.Email);
                throw new AssignRoleFailedException(user.Email, _defaultRole);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("DomainSerVice: Đăng ký user {Email} bên thứ 3 thành công!", request.Email);
            return user;
        }

        // Login tài khoản từ bên ngoài nếu email chưa có đăng ký
        public async Task<User> ProviderLoginUserAsync(User user, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DomainSerVice: Bắt đầu đăng nhập user {Email} bên thứ 3", user.Email);

            // Kiểm tra user đã tồn tại chưa
            var roles = await _unitOfWork.Users.GetRolesUserAsync(user);
            if (roles == null || !roles.Any())
            {
                _logger.LogError("DomainSerVice: User {Email} không có vai trò!", user.Email);
                throw new InvalidUserHasNoRoleException(user.Email);
            }
            // Trả về kết quả login
            user.Roles = roles.ToList();
            _logger.LogInformation("DomainSerVice: Đăng nhập user {Email} bên thứ 3 thành công!", user.Email);
            return user;
        }

        // Update Info người dùng
        public async Task<User> UpdateInfoUserAsync(InfoUserUpdateDTO update,string userId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DomainSerVice: Bắt đầu cập nhật thông tin user {UserName}", update.UserName);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Tìm người dùng theo userId
            var user = await _unitOfWork.Users.FindByUserIdAsync(userId,cancellationToken);
            if (user == null)
            {
                _logger.LogWarning("DomainSerVice: Người dùng không tồn tại với Id {UserId}", userId);
                throw new UserNotFoundException(update.UserName);
            }
            user.UserName = update.UserName;
            user.PhoneNumber = update.PhoneNumber;

            // Check xem người dùng có thay đổi avatar không
            if (update.AvatarND != null && update.AvatarND.Length > 0)
            {
                // Thư mục lưu trữ ảnh
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "avatars");

                // Nếu User có avatar cũ thì xóa
                if (!string.IsNullOrEmpty(user.AvatarND))
                {
                    FileHelper.DeleteImage(user.AvatarND);
                }

                // Lưu ảnh mới
                var fileName = await FileHelper.SaveImageAsync(update.AvatarND, folderPath);
                user.AvatarND = Path.Combine("uploads", "avatars", fileName).Replace("\\", "/"); // Lưu đường dẫn tương đối

            }

            user.NameND = update.NameND;
            user.UserName = update.UserName;
            user.GioiTinh = update.GioiTinh;
            user.MoTa = update.MoTa;

            // Cập nhật thông tin người dùng
            var resultUpdate = await _unitOfWork.Users.UpdateUserAsync(user);
            if (resultUpdate == null)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError("DomainSerVice: Cập nhật thông tin người dùng {UserName} thất bại!", user.UserName);
                throw new UpdateUserFailedException(update.UserName);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("DomainSerVice: Cập nhật thông tin người dùng {UserName} thành công!", user.UserName);
            return user;
        }

        // Login Local
        public async Task<User> LoginUserAsync(User user, LoginRequestDTO requestLogin,CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DomainSerVice: Bắt đầu đăng nhập user {Account}", requestLogin.Account);

            // Kiểm tra user đã tồn tại chưa
            var CheckPass = await _unitOfWork.Users.CheckPasswordAsync(user, requestLogin.Password);
            if (!CheckPass)
            {
                _logger.LogWarning("DomainSerVice: Đăng nhập user {Account} thất bại do sai mật khẩu!", requestLogin.Account);
                throw new InvalidPasswordException();
            }

            // Kiểm tra user có Role không
            var roles = await _unitOfWork.Users.GetRolesUserAsync(user);
            if (roles == null || !roles.Any())
            {
                _logger.LogError("DomainSerVice: User {Account} không có vai trò!", requestLogin.Account);
                throw new InvalidUserHasNoRoleException(requestLogin.Account);
            }
            // Trả về kết quả login
            user.Roles = roles.ToList();
            _logger.LogInformation("DomainSerVice: Đăng nhập user {Account} thành công!", requestLogin.Account);
            return user;
        }

        // Tìm User Dựa vào UserId
        public async Task<User> FindUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DomainSerVice: Tìm kiếm người dùng với Email {Email}", email);
            // Tìm người dùng theo email
            var user = await _unitOfWork.Users.FindByEmailAsync(email, cancellationToken);
            if (user == null)
            {
                _logger.LogWarning("DomainSerVice: Người dùng không tồn tại với Email {Email}", email);
                throw new UserNotFoundException(email);
            }
            _logger.LogInformation("DomainSerVice: Tìm thấy người dùng với Email {Email}", email);
            return user;
        }

        // Đặt lại mật khẩu cho người dùng
        public async Task<User> ResetPasswordAsync(string userId,string newPassword, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("DomainSerVice: Tìm kiếm người dùng với UserName");
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Tìm người dùng theo userId
            var userResponse = await _unitOfWork.Users.FindByUserIdAsync(userId, cancellationToken);
            if (userResponse == null)
            {
                _logger.LogWarning("DomainSerVice: Người dùng không tồn tại");
                throw new UserNotFoundException();
            }

            // Check mật khẩu mới không được trùng với mật khẩu cũ
            var checkPass = await _unitOfWork.Users.CheckPasswordAsync(userResponse, newPassword);
            if (checkPass)
            {
                _logger.LogWarning("DomainSerVice: Mật khẩu mới không được trùng với mật khẩu cũ của người dùng {UserName}", userResponse.UserName);
                throw new DuplicateResetPasswordException(userResponse.Email);
            }

            // Tạo token đặt lại mật khẩu
            var tokenReset = await _unitOfWork.Users.GeneratePasswordResetTokenAsync(userResponse);
            if(string.IsNullOrEmpty(tokenReset))
            {
                _logger.LogError("DomainSerVice: Tạo token đặt lại mật khẩu cho người dùng {UserName} thất bại!", userResponse.UserName);
                throw new GenerateResetTokenFailedException(userResponse.Email);
            }

            // Đặt lại mật khẩu với token
            var resultReset = await _unitOfWork.Users.ResetPasswordWithTokenAsync(userResponse, tokenReset, newPassword);
            if (!resultReset.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError("DomainSerVice: Đặt lại mật khẩu cho người dùng {UserName} thất bại!", userResponse.UserName);
                throw new ResetPasswordFailedException(userResponse.Email);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("DomainSerVice: Tìm thấy người dùng với UserName {UserName}", userResponse.Email);
            return userResponse;
        }
    }
}
