namespace GienPhim_Services.InteractionService.Application.DTOs.Client.ChatGroup
{
    public class ChatGroupResponseDTO
    {
        public Guid GroupId { get; set; }

        public int PhimId { get; set; }
        public string PhimName { get; set; } = string.Empty;

        public string CreatedByUserId { get; set; } = string.Empty;

        // Cài đặt
        public bool AllowChat { get; set; }
        public bool AllowInvite { get; set; }
        public bool AllowJoinByLink { get; set; }

        // Playback
        public bool IsPlaying { get; set; }
        public TimeSpan CurrentPlaybackPosition { get; set; }
        public DateTime? LastPlaybackUpdated { get; set; }

        // Thời gian
        public DateTime ShowStartTime { get; set; }
        public DateTime ShowEndTime { get; set; }
        public DateTime AutoDeleteAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }

        // Invite
        public string? InviteCode { get; set; }
        public DateTime? InviteCodeExpireAt { get; set; }

        // (Optional) Đếm số thành viên để hiển thị list
        public int MemberCount { get; set; }
    }
}
