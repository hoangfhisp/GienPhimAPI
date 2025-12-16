using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.LichSuXem
{
    public class GetLichSuXemUserRequestDTO
    {
        [Required(ErrorMessage = "UserId là bắt buộc nhập")]
        public string UserId { get; set; }
        public int page { get; set; } = 1;
        public int limit { get; set; } = 10;

    }
}
