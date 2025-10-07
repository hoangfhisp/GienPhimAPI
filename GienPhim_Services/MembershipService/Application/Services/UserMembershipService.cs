using GienPhim_Services.MembershipService.Application.DTOs.Client.UserMembership;
using GienPhim_Services.MembershipService.Application.DTOs.Common;
using GienPhim_Services.MembershipService.Application.Exceptions.Failed;
using GienPhim_Services.MembershipService.Application.Exceptions.NotFounds;
using GienPhim_Services.MembershipService.Application.Interfaces.Mappers;
using GienPhim_Services.MembershipService.Application.Interfaces.Orchestrators;
using GienPhim_Services.MembershipService.Application.Interfaces.Repositories;
using GienPhim_Services.MembershipService.Application.Interfaces.Services;

namespace GienPhim_Services.MembershipService.Application.Services
{
    public class UserMembershipService : IUserMembershipService
    {
        private readonly IUserMembershipOrchestrator _userMembershipOrchestrator;
        private readonly IUserMembershipResponseMapper _userMembershipResponseMapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserMembershipService> _logger;

        public UserMembershipService(
            IUnitOfWork unitOfWork,
            IUserMembershipOrchestrator userMembershipOrchestrator,
            IUserMembershipResponseMapper userMembershipResponseMapper,
            IPaginationMapper paginationMapper,
            ILogger<UserMembershipService> logger)
        {
            _unitOfWork = unitOfWork;
            _userMembershipOrchestrator = userMembershipOrchestrator;
            _userMembershipResponseMapper = userMembershipResponseMapper;
            _paginationMapper = paginationMapper;
            _logger = logger;
        }

        // Lấy danh sách UserMembership có phân trang
        public async Task<ResultListDTO<List<UserMembershipResponseDTO>>> GetAllUserMembershipsAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetAllUserMembershipsAsync với page={Page}, limit={Limit}", page, limit);

            var (memberships, total, last) = await _unitOfWork.UserMemberships.GetAllUserMembershipsAsync(page, limit, cancellationToken);

            if (total == 0)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy UserMembership nào");
                throw new UserMembershipNotFoundException();
            }

            var dtoList = _userMembershipResponseMapper.ToDtoList(memberships);
            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            _logger.LogInformation("ApplicationService: Lấy danh sách UserMembership thành công, tổng={Total}", total);

            return ResultListDTO<List<UserMembershipResponseDTO>>.Success(
                dtoList,
                "Lấy danh sách UserMembership thành công",
                pagination
            );
        }

        // Lấy chi tiết UserMembership
        public async Task<ResultDTO<UserMembershipResponseDTO>> GetDetailAsync(string userMembershipId, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetDetailAsync cho UserMembershipId={UserMembershipId}", userMembershipId);

            var membership = await _unitOfWork.UserMemberships.GetByIdAsync(userMembershipId, ct);
            if (membership is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy UserMembership Id={UserMembershipId}", userMembershipId);
                throw new UserMembershipNotFoundException(userMembershipId);
            }

            var dto = _userMembershipResponseMapper.ToDto(membership);

            _logger.LogInformation("ApplicationService: Lấy chi tiết UserMembership Id={UserMembershipId} thành công", userMembershipId);

            return ResultDTO<UserMembershipResponseDTO>.Success(dto, "Lấy chi tiết gói thành viên người dùng thành công");
        }

        // Tạo mới UserMembership
        public async Task<ResultDTO> CreateUserMembershipAsync(UserMembershipCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu CreateUserMembershipAsync cho UserId={UserId}, OptionId={OptionId}", createDto.UserId, createDto.OptionId);

            // Kiểm tra PlanOption tồn tại
            var existingOption = await _unitOfWork.PlanOptions.GetByIdAsync(createDto.OptionId, cancellationToken);
            if (existingOption is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy PlanOption Id={OptionId}", createDto.OptionId);
                throw new PlanOptionNotFoundException(createDto.OptionId);
            }

            // Tạo entity qua orchestrator
            var userMembership = await _userMembershipOrchestrator.CreateUserMembershipAsync(createDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.UserMemberships.CreateAsync(userMembership, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Tạo UserMembership thất bại cho UserId={UserId}", createDto.UserId);
                await transaction.RollbackAsync(cancellationToken);
                throw new CreateUserMembershipFailedException(createDto.UserId);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Tạo UserMembership thành công cho UserId={UserId}", createDto.UserId);

            return ResultDTO.Success($"Tạo gói thành viên cho người dùng {createDto.UserId} thành công");
        }

        // Cập nhật UserMembership
        public async Task<ResultDTO<UpdateUserMembershipResponseDTO>> UpdateUserMembershipAsync(UserMembershipUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu UpdateUserMembershipAsync với Id={UserMembershipId}", updateDto.UserMembershipId);

            // Kiểm tra OptionId mới có hợp lệ không
            if (!string.IsNullOrEmpty(updateDto.OptionId))
            {
                var existingOption = await _unitOfWork.PlanOptions.GetByIdAsync(updateDto.OptionId, cancellationToken);
                if (existingOption is null)
                {
                    _logger.LogWarning("ApplicationService: Không tìm thấy PlanOption Id={OptionId}", updateDto.OptionId);
                    throw new PlanOptionNotFoundException(updateDto.OptionId);
                }
            }

            var membership = await _userMembershipOrchestrator.UpdateUserMembershipAsync(updateDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.UserMemberships.UpdateAsync(membership, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Cập nhật UserMembership thất bại Id={UserMembershipId}", updateDto.UserMembershipId);
                await transaction.RollbackAsync(cancellationToken);
                throw new UpdateUserMembershipFailedException(updateDto.UserMembershipId);
            }

            await transaction.CommitAsync(cancellationToken);

            var response = _userMembershipResponseMapper.ToUpdateDto(membership);

            _logger.LogInformation("ApplicationService: Cập nhật UserMembership thành công Id={UserMembershipId}", updateDto.UserMembershipId);

            return ResultDTO<UpdateUserMembershipResponseDTO>.Success(response, $"Cập nhật gói thành viên người dùng thành công");
        }

        // Xóa UserMembership
        public async Task<ResultDTO> DeleteUserMembershipAsync(UserMembershipDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu DeleteUserMembershipAsync với Id={UserMembershipId}", deleteDto.UserMembershipId);

            await _userMembershipOrchestrator.DeleteUserMembershipAsync(deleteDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var membership = await _unitOfWork.UserMemberships.GetByIdAsync(deleteDto.UserMembershipId, cancellationToken);
            if (membership is null)
            {
                _logger.LogWarning("ApplicationService: UserMembership không tồn tại khi xóa Id={UserMembershipId}", deleteDto.UserMembershipId);
                return ResultDTO.Fail($"Không tìm thấy gói thành viên người dùng với Id {deleteDto.UserMembershipId}");
            }

            await _unitOfWork.UserMemberships.DeleteAsync(membership, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("ApplicationService: Xóa UserMembership thành công Id={UserMembershipId}", deleteDto.UserMembershipId);
            return ResultDTO.Success($"Xóa gói thành viên người dùng {deleteDto.UserMembershipId} thành công");
        }
    }
}
