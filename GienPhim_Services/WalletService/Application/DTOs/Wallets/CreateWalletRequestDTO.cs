using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.WalletService.Application.DTOs.Wallets
{
    public class CreateWalletRequestDTO
    {
        [Required(ErrorMessage = "UserId là bắt buộc nhập!")]
        public string UserId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Số dư là bắt buộc nhập!")]
        public decimal SoDu { get; set; } = 0;
    }
}
