using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.InteractionService.Application.DTOs.Client.ChatGroup
{
    public class ChatGroupCreateDTO
    {
        [Required(ErrorMessage = "Thiếu thông tin phim để tạo phòng.")]
        public int PhimId { get; set; }

        // Tạm thời yêu cầu cho dễ, sau này có FilmService thì có thể relax/bỏ
        [Required(ErrorMessage = "Tên phim không được để trống.")]
        [MaxLength(ChatGroupDtoInvariants.PhimNameMaxLength, ErrorMessage = "Tên phim không được vượt quá {1} ký tự.")]
        public string PhimName { get; set; } = string.Empty;

        // Nếu sau này bạn muốn cho user chọn kiểu phòng, có thể thêm các field khác ở đây
        // ví dụ: IsPrivate, Note,... (tuỳ business)
    }
}
