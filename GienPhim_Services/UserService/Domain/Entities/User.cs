using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GienPhim_Services.UserService.Domain.Entities
{
    public class User : IdentityUser<string>
    {
        public string? NameND { get; set; }
        public int? GioiTinh { get; set; }

        [MaxLength(500)]
        public string? MoTa { get; set; }

        public string? AvatarND { get; set; }

        // Thêm để hỗ trợ login từ provider
        public string? AuthId { get; set; } // ID từ Google/Facebook/...
        public string TypeLogin { get; set; } = "Local"; // "Google", "Facebook",...

        public DateTime ThoiGianTao { get; set; } = DateTime.Now;

        public DateTime? ThoiGianCapNhat { get; set; }

        public DateTime ThoiGianDoiEmail { get; set; }
        [NotMapped]
        public List<string> Roles { get; set; } = new List<string>();
    }
}
