using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Domain.Entities;

namespace GienPhim_Services.UserService.Application.Interfaces.Factories
{
    public interface IUserFactory
    {
        User CreateLocalUser(UserRegisterDTO userRegister, string userName); // Tạo user local từ thông tin đăng ký
        User CreateExternalUser(ExternalLoginUserInfoDTO externalUser, string userName); // Tạo user từ thông tin đăng nhập bên thứ 3 (Google, Facebook,...)
        User CreateUpdateInfoUser(InfoUserUpdateDTO update, string userId); // Tạo user từ thông tin cập nhật
        User CreateEmail(FindUserByEmailDTO dto); // Tạo user từ email để tìm kiếm
        User CreateUserId(FindUserByIdDTO dto); // Tạo user từ userId để tìm kiếm
        User CreateResetPassword(ResetPasswordRequestDTO dto); // Tạo user từ thông tin reset mật khẩu
    }
}
