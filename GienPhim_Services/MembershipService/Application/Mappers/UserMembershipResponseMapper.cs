using GienPhim_Services.MembershipService.Application.DTOs.Client.UserMembership;
using GienPhim_Services.MembershipService.Application.Interfaces.Mappers;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Mappers
{
    public class UserMembershipResponseMapper : IUserMembershipResponseMapper
    {
        public UserMembershipResponseDTO ToDto(UserMembership userMembership)
        {
            return new UserMembershipResponseDTO
            {
                UserMembershipId = userMembership.UserMembershipId,
                OptionId = userMembership.OptionId,
                UserId = userMembership.UserId,
                NgayBatDau = userMembership.NgayBatDau,
                NgayKetThuc = userMembership.NgayKetThuc,
                TrangThai = userMembership.TrangThai
            };
        }

        public List<UserMembershipResponseDTO> ToDtoList(List<UserMembership> userMemberships)
        {
            return userMemberships.Select(ToDto).ToList();
        }

        public UpdateUserMembershipResponseDTO ToUpdateDto(UserMembership userMembership)
        {
            return new UpdateUserMembershipResponseDTO
            {
                UserMembershipId = userMembership.UserMembershipId,
                OptionId = userMembership.OptionId,
                UserId = userMembership.UserId,
                NgayBatDau = userMembership.NgayBatDau,
                NgayKetThuc = userMembership.NgayKetThuc,
                TrangThai = userMembership.TrangThai
            };
        }

        public List<UpdateUserMembershipResponseDTO> ToUpdateDtoList(List<UserMembership> userMemberships)
        {
            return userMemberships.Select(ToUpdateDto).ToList();
        }
    }
}
