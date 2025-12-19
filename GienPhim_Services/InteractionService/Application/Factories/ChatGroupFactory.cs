using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatGroup;
using GienPhim_Services.InteractionService.Application.Interfaces.Factories;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Factories
{
    public class ChatGroupFactory : IChatGroupFactory
    {
        // Hard-code thời lượng, sau này nếu muốn có thể chuyển sang config
        private const int DefaultShowDurationHours = 3;  // ví dụ phim 3 tiếng
        private const int DefaultAutoDeleteOffsetHours = 5; // sau 5 tiếng tự động xoá phòng

        public ChatGroup CreateChatGroup(ChatGroupCreateDTO dto, string currentUserId, DateTime nowUtc)
        {
            // Thời gian
            var showStart = nowUtc;
            var showEnd = nowUtc.AddHours(DefaultShowDurationHours);
            var autoDeleteAt = nowUtc.AddHours(DefaultAutoDeleteOffsetHours);

            var inviteCode = GenerateInviteCode();

            var group = new ChatGroup
            {
                GroupId = Guid.NewGuid(),

                PhimId = dto.PhimId,
                PhimName = dto.PhimName?.Trim() ?? string.Empty,

                CreatedByUserId = currentUserId,

                // Cài đặt mặc định
                AllowChat = true,
                AllowInvite = true,
                AllowJoinByLink = true,

                // Playback
                IsPlaying = false,
                CurrentPlaybackPosition = TimeSpan.Zero,
                LastPlaybackUpdated = null,

                ShowStartTime = showStart,
                ShowEndTime = showEnd,
                AutoDeleteAt = autoDeleteAt,
                CreatedAt = nowUtc,

                IsDeleted = false,

                InviteCode = inviteCode,
                InviteCodeExpireAt = autoDeleteAt,

                Members = new List<GroupMember>(),
                Messages = new List<ChatMessage>(),
                JoinRequests = new List<JoinRequest>()
            };

            // ADD OWNER MEMBER
            var ownerMember = new GroupMember
            {
                GroupMemberId = Guid.NewGuid(),
                GroupId = group.GroupId,
                UserId = currentUserId,

                IsOwner = true,
                CanChat = true,
                CanControlFilm = true,
                IsMuted = false,

                JoinedAt = nowUtc
            };

            group.Members.Add(ownerMember);

            return group;
        }

        private string GenerateInviteCode()
        {
            // Ví dụ: 8 ký tự chữ/ số lấy từ Guid
            return Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
        }
    }
}
