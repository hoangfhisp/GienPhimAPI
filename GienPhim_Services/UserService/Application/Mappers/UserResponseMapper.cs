using GienPhim_Services.UserService.Application.DTOs.Client.Login;
using GienPhim_Services.UserService.Application.DTOs.Client.User;
using GienPhim_Services.UserService.Application.Interfaces.Mappers;
using GienPhim_Services.UserService.Domain.Entities;

namespace GienPhim_Services.UserService.Application.Mappers
{
    public class UserResponseMapper : IUserResponseMapper
    {
        // Map Cập nhật thông tin user
        public UpdateInfoUserResponseDTO ToUpdate(User user)
        {
            return new UpdateInfoUserResponseDTO
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                NameND = user.NameND,
                GioiTinh = user.GioiTinh ?? 0,
                Mota = user.MoTa,
                AvatarND = user.AvatarND,
                ThoiGianCapNhat = user.ThoiGianCapNhat ?? DateTime.Now,
                ThoiGianTao = user.ThoiGianTao
            };
        }

        public ExternalLoginUserResponseDTO ToExternalLogin(User user)
        {
            return new ExternalLoginUserResponseDTO
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Roles.FirstOrDefault(),
            };
        }

        public LoginLocalUserResponseDTO ToLocalLogin(User user)
        {
            return new LoginLocalUserResponseDTO
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Roles.FirstOrDefault(),
            };
        }

        public UserResponseDTO ToDto(User user)
        {
            return new UserResponseDTO
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                NameND = user.NameND,
                GioiTinh = user.GioiTinh ?? 0,
                Mota = user.MoTa,
                AvatarND = user.AvatarND,
                TypeLogin = user.TypeLogin,
                ThoiGianCapNhat = user.ThoiGianCapNhat ?? DateTime.Now,
                ThoiGianTao = user.ThoiGianTao
            };
        }
    }
}
