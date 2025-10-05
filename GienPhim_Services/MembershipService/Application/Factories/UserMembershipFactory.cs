using GienPhim_Services.MembershipService.Application.DTOs.Client.UserMembership;
using GienPhim_Services.MembershipService.Application.Interfaces.Factories;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Factories
{
    public class UserMembershipFactory : IUserMembershipFactory
    {
        public UserMembership CreateUserMembership(UserMembershipCreateDTO dto)
        {
            return new UserMembership
            {
                UserMembershipId = Guid.NewGuid().ToString(),
                OptionId = dto.OptionId.Trim(),
                UserId = dto.UserId.Trim(),
                NgayBatDau = dto.NgayBatDau,
                NgayKetThuc = dto.NgayKetThuc,
                TrangThai = dto.TrangThai.Trim()
            };
        }

        public UserMembership UpdateUserMembership(UserMembershipUpdateDTO dto)
        {
            return new UserMembership
            {
                UserMembershipId = dto.UserMembershipId.Trim(),
                OptionId = dto.OptionId.Trim(),
                UserId = dto.UserId.Trim(),
                NgayBatDau = dto.NgayBatDau,
                NgayKetThuc = dto.NgayKetThuc,
                TrangThai = dto.TrangThai.Trim()
            };
        }

        public UserMembership DeleteUserMembership(UserMembershipDeleteDTO dto)
        {
            return new UserMembership
            {
                UserMembershipId = dto.UserMembershipId.Trim()
            };
        }
    }
}
