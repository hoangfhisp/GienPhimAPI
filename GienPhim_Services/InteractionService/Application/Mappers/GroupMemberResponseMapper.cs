using GienPhim_Services.InteractionService.Application.DTOs.Client.GroupMember;
using GienPhim_Services.InteractionService.Application.Interfaces.Mappers;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Mappers
{
    public class GroupMemberResponseMapper : IGroupMemberResponseMapper
    {
        public GroupMemberResponseDTO ToResponseDto(GroupMember member)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));

            return new GroupMemberResponseDTO
            {
                GroupMemberId = member.GroupMemberId,
                GroupId = member.GroupId,
                UserId = member.UserId,
                IsOwner = member.IsOwner,
                CanChat = member.CanChat,
                CanControlFilm = member.CanControlFilm,
                IsMuted = member.IsMuted,
                JoinedAt = member.JoinedAt
            };
        }

        public List<GroupMemberResponseDTO> ToResponseDtoList(IEnumerable<GroupMember> members)
        {
            return members.Select(ToResponseDto).ToList();
        }
    }
}
