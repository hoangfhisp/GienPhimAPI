using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.InteractionService.Application.DTOs.Client.Phim
{
    public class UpdatePhimLikeDisLikeRequestDTO
    {
        [Required(ErrorMessage = "Hãy nhập ID phim!")]
        public int PhimId { get; set; }
        [Required(ErrorMessage = "Hãy nhập trạng thái thích hoặc không thích!")]
        public bool IsLike { get; set; }
        [Required(ErrorMessage = "Hãy nhập trạng thái thích hoặc không thích!")]
        public bool IsDislike { get; set; }
        public bool? OldIsLike { get; set; }
        public bool? OldIsDislike { get; set; }
    }
}
