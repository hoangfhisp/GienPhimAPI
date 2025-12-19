using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.InteractionService.Domain.Entities
{
    public class JoinRequest
    {
        [Key]
        public Guid RequestId { get; set; }

        [Required]
        public Guid GroupId { get; set; }

        [ForeignKey("GroupId")]
        public ChatGroup Group { get; set; }

        [Required]
        public string RequestUserId { get; set; } = "";

        public string Status { get; set; } = "Pending"; // Pending / Accepted / Rejected
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; }
    }
}
