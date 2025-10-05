using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.MembershipService.Application.DTOs.Client.PlanOption
{
    public class PlanOptionCreateDTO
    {
        [Required(ErrorMessage = "Thiếu PlanId")]
        public string PlanId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Thời gian sử dụng không được để trống")]
        [Range(DtoInvariants.ThoiGianSuDungMin, DtoInvariants.ThoiGianSuDungMax, ErrorMessage = "Thời gian sử dụng phải nằm trong khoảng {1} - {2} ngày")]
        public int ThoiGianSuDung { get; set; }

        [Required(ErrorMessage = "Giá gói thành viên không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá gói thành viên không hợp lệ")]
        public decimal GiaOption { get; set; }
    }
}
