using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.InteractionService.Domain.Entities
{
    public class GroupMember
    {
        [Key]
        public Guid GroupMemberId { get; set; }

        [Required]
        public Guid GroupId { get; set; }
        [ForeignKey("GroupId")]
        public ChatGroup Group { get; set; }

        [Required]
        public string UserId { get; set; } = "";

        public bool IsOwner { get; set; } = false;
        public bool CanChat { get; set; } = true;     // Có thể gửi tin nhắn không
        public bool CanControlFilm { get; set; } = false; // Có thể điều khiển phim không
        public bool IsMuted { get; set; } = false;    // Bị chủ group tắt chat cá nhân

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
