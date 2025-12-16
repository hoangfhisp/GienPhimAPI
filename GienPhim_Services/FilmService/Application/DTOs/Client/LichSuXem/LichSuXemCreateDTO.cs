using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.LichSuXem
{
    public class LichSuXemCreateDTO
    {
        [Required(ErrorMessage = "PhimId là bắt buộc nhập!")]
        public int PhimId { get; set; }

        [Required(ErrorMessage = "UserId là bắt buộc nhập!")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Tập vừa xem là bắt buộc nhập!")]
        public string TapVuaXem { get; set; }
    }
}
