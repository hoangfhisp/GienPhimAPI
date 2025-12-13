using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.InteractionService.Domain.Entities
{
    public class ChatMessage
    {
        [Key]
        public Guid MessageId { get; set; }

        [Required]
        public Guid GroupId { get; set; }
        [ForeignKey("GroupId")]
        public ChatGroup Group { get; set; }

        [Required]
        public string UserId { get; set; } = "";

        [Required]
        [MaxLength(500)]
        public string Content { get; set; } = "";

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public bool IsSystemMessage { get; set; } = false; // Tin nhắn hệ thống như hiển thị: A vừa tham gia group chat
        public string? LoaiTinNhan { get; set; } // text, image, voice,...
    }
}
