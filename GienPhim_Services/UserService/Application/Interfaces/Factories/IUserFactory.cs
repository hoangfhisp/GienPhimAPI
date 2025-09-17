using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Domain.Entities;

namespace GienPhim_Services.UserService.Application.Interfaces.Factories
{
    public interface IUserFactory
    {
        User CreateLocalUser(UserRegisterDTO userRegister, string userName); // Tạo user local từ thông tin đăng ký
    }
}
