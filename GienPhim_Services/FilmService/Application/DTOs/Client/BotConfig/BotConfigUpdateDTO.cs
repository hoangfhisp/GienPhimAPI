using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.BotConfig
{
    public class BotConfigUpdateDTO
    {
        [Required(ErrorMessage = "Trạng thái hoạt động là bắt buộc.")]
        public bool IsEnabled { get; set; }

        [Required(ErrorMessage = "Số lượng API mỗi lần chạy là bắt buộc.")]
        [Range(1, 100, ErrorMessage = "Giá trị ApiPerRun phải nằm trong khoảng từ 1 đến 100.")]
        public int ApiPerRun { get; set; }

        [Required(ErrorMessage = "Khoảng thời gian chạy lại bot là bắt buộc.")]
        [Range(1, 24, ErrorMessage = "IntervalHours phải nằm trong khoảng từ 1 đến 24 giờ.")]
        public int IntervalHours { get; set; }
    }
}
