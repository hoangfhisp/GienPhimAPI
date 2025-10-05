using GienPhim_Services.MembershipService.Application.DTOs.Client.UserMembership;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Mappers
{
    public interface IUserMembershipResponseMapper
    {
        UserMembershipResponseDTO ToDto(UserMembership userMembership);
        List<UserMembershipResponseDTO> ToDtoList(List<UserMembership> userMemberships);
        UpdateUserMembershipResponseDTO ToUpdateDto(UserMembership userMembership);
        List<UpdateUserMembershipResponseDTO> ToUpdateDtoList(List<UserMembership> userMemberships);
    }
}
