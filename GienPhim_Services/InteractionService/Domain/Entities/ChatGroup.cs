using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GienPhim_Services.InteractionService.Domain.Entities
{
    public class ChatGroup
    {
        [Key]
        public Guid GroupId { get; set; }

        [Required]
        public int PhimId { get; set; }

        [Required]
        public string PhimName { get; set; } = string.Empty;

        public string CreatedByUserId { get; set; } = string.Empty; // Chủ group

        // Điều khiển của chủ group
        public bool AllowChat { get; set; } = true;      // Có cho chat không
        public bool AllowInvite { get; set; } = true;    // Có cho phép mời thêm không
        public bool AllowJoinByLink { get; set; } = true;// Có bật link mời không

        // Trạng thái phim (điều khiển playback)
        public bool IsPlaying { get; set; } = false;
        public TimeSpan CurrentPlaybackPosition { get; set; } = TimeSpan.Zero; // Tua tới đâu
        public DateTime? LastPlaybackUpdated { get; set; } // Lưu mốc thời gian tua

        public DateTime ShowStartTime { get; set; }
        public DateTime ShowEndTime { get; set; }
        public DateTime AutoDeleteAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        // Link mời
        public string? InviteCode { get; set; }
        public DateTime? InviteCodeExpireAt { get; set; }

        // Navigation
        public ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();
        public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
        public ICollection<JoinRequest> JoinRequests { get; set; } = new List<JoinRequest>();
    }
}
