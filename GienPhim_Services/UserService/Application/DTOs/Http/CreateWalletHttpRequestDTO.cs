using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.UserService.Application.DTOs.Http
{
    public class CreateWalletHttpRequestDTO
    {
        [Required(ErrorMessage = "UserId là bắt buộc nhập!")]
        public string UserId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Số dư là bắt buộc nhập!")]
        public decimal SoDu { get; set; } = 0;
    }
}
