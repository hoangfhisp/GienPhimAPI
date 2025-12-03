using GienPhim_Services.InteractionService.Application.Interfaces.Factories;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Factories
{
    public class GroupMemberFactory : IGroupMemberFactory
    {
        public GroupMember CreateForJoin(ChatGroup group, string userId, DateTime joinedAt, bool isOwner = false)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));

            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId không được để trống", nameof(userId));

            return new GroupMember
            {
                GroupMemberId = Guid.NewGuid(),
                GroupId = group.GroupId,
                UserId = userId,
                IsOwner = isOwner,
                CanChat = true,
                CanControlFilm = isOwner, // Chủ phòng mặc định được điểu khiển film
                IsMuted = false,
                JoinedAt = joinedAt
            };
        }
    }
}
