using GienPhim_Services.UserService.Domain.Entities;
using GienPhim_Services.UserService.Domain.Exceptions.Invalid;
using GienPhim_Services.UserService.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GienPhim_Services.UserService.Domain.Services
{
    // Chỉ chứa nghiệp vụ thuần, không repo, không log, không transaction
    public class UserDomainService : IUserDomainService
    { 

        // Đăng ký tài khoản Local
        public void EnsureCanRegister(User user)
        {
            if (user == null)
                throw new InvalidUserException(user?.Email);

            ValidateEmail(user.Email);
        }

        // Đăng ký tài khoản từ bên ngoài
        public void EnsureCanProviderRegister(User user)
        {
            if (user == null)
                throw new InvalidUserException(user?.Email);

            // Kiểm tra email
            ValidateEmail(user.Email);

            if (string.IsNullOrWhiteSpace(user.AuthId))
                throw new InvalidUserAuthIdException(user.AuthId);

            if (string.IsNullOrWhiteSpace(user.TypeLogin))
                throw new InvalidUserTypeLoginException(user.TypeLogin);
        }

        // Đăng nhập tài khoản từ bên ngoài
        public void EnsureCanProviderLogin(User user)
        {
            if (user == null)
                throw new InvalidUserException(user?.Email);

            ValidateEmail(user.Email);

            if (string.IsNullOrWhiteSpace(user.AuthId))
                throw new InvalidUserAuthIdException(user.AuthId);

            if (string.IsNullOrWhiteSpace(user.TypeLogin))
                throw new InvalidUserTypeLoginException(user.TypeLogin);
        }

        // Update Info người dùng
        public void EnsureCanUpdateInfoUser(User user)
        {
            if (user == null)
                throw new InvalidUserException(user?.Email);

            if (string.IsNullOrWhiteSpace(user.UserName))
                throw new InvalidUserNameException(user.UserName);

            if (user.UserName.Length < 3 || user.UserName.Length > 50)
                throw new InvalidUserNameException(user.UserName);

            if (string.IsNullOrWhiteSpace(user.PhoneNumber) || user.PhoneNumber.Length < 10 || user.PhoneNumber.Length > 11)
                throw new InvalidUserPhoneNumberException(user.PhoneNumber);
        }

        // Login Local
        public void EnsureCanLoginLocal(User user)
        {
            if (user == null)
                throw new InvalidUserException(user?.Email);

            ValidateEmail(user.Email);
        }


        // Tìm User Dựa vào email
        public void EnsureCanUserEmail(User user)
        {
            if (user == null)
                throw new InvalidUserException(user?.Email);

            
        }

        // Tìm User Dựa vào userId
        public void EnsureCanResetPassword(User user)
        {
            if (user == null)
                throw new InvalidUserException(user?.Email);

            if (string.IsNullOrWhiteSpace(user.Id))
                throw new InvalidUserIdException(user.Id);

        }

        // private methods
        private void ValidateEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidUserEmailException(email);

            if (!email.Contains("@"))
                throw new InvalidUserEmailException(email);
        }
    }
}
