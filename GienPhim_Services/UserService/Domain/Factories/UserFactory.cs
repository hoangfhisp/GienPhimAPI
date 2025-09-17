using Azure.Core;
using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Domain.Entities;
using GienPhim_Services.UserService.Domain.Factories.Interfaces;

namespace GienPhim_Services.UserService.Domain.Factories
{
    public class UserFactory : IUserFactory
    {
        // Tạo user từ thông tin đăng nhập bên thứ 3 (Google, Facebook,...)
        public User CreateExternalUser(ExternalLoginUserInfoDTO externalUser, string userName)
        {
            return new User
            {
                Id = Guid.NewGuid().ToString("N"),
                Email = externalUser.Email,
                NameND = externalUser.NameND,
                UserName = userName,
                AvatarND = externalUser.AvatarND,
                TypeLogin = externalUser.TypeLogin,
                AuthId = externalUser.AuthId,
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }
    }
}
