using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Application.Interfaces.Factories;
using GienPhim_Services.UserService.Domain.Entities;

namespace GienPhim_Services.UserService.Application.Factories
{
    public class UserFactory : IUserFactory
    {
        // Tạo user local từ thông tin đăng ký
        public User CreateLocalUser(UserRegisterDTO userRegister, string userName)
        {
            return new User
            {
                Id = Guid.NewGuid().ToString("N"),
                Email = userRegister.Email,
                UserName = userName,
                PhoneNumber = userRegister.PhoneNumber,
                TypeLogin = "Local",
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }
    }
}
