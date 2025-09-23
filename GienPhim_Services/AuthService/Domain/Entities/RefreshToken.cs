using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.AuthService.Domain.Entities
{
    public class RefreshToken
    {
        [Key]
        public string RFTokenId { get; set; } // Primary Key
        public string UserId { get; set; }// Foreign Key
        public string Token { get; set; } // Token
        public DateTime ExpiredAt { get; set; } // Expiration time
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Creation time
        public DateTime? RevokedAt { get; set; } // Revocation time

        public bool IsActive => RevokedAt == null && DateTime.UtcNow < ExpiredAt;
    }
}
