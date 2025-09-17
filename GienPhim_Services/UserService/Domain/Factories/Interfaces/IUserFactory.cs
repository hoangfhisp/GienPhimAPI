using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Domain.Entities;

namespace GienPhim_Services.UserService.Domain.Factories.Interfaces
{
    public interface IUserFactory
    {
        User CreateExternalUser(ExternalLoginUserInfoDTO externalUser, string userName); // Tạo user từ thông tin đăng nhập bên thứ 3 (Google, Facebook,...)
    }
}
