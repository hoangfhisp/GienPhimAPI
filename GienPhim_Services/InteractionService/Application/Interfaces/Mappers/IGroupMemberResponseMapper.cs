using GienPhim_Services.InteractionService.Application.DTOs.Client.GroupMember;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Mappers
{
    public interface IGroupMemberResponseMapper
    {
        GroupMemberResponseDTO ToResponseDto(GroupMember member);
        List<GroupMemberResponseDTO> ToResponseDtoList(IEnumerable<GroupMember> members);
    }
}
