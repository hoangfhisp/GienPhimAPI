using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.MembershipService.Application.DTOs.Client.UserMembership
{
    public class UserMembershipCreateDTO
    {
        [Required(ErrorMessage = "Thiếu OptionId")]
        public string OptionId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Thiếu UserId")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Thiếu ngày bắt đầu")]
        public DateTime NgayBatDau { get; set; }

        [Required(ErrorMessage = "Thiếu ngày kết thúc")]
        public DateTime NgayKetThuc { get; set; }

        [Required(ErrorMessage = "Thiếu trạng thái gói thành viên")]
        [StringLength(DtoInvariants.TrangThaiMaxLength, ErrorMessage = "Trạng thái không được vượt quá {1} ký tự")]
        public string TrangThai { get; set; } = string.Empty;
    }
}
