using GienPhim_Services.WalletService.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.WalletService.Application.DTOs.Wallets
{
    public class UpdateWalletRequestDTO
    {
        [Required(ErrorMessage = "WalletId là bắt buộc nhập!")]
        public string WalletId { get; set; } // Ma ví tiền
        [Required(ErrorMessage = "UserId là bắt buộc nhập!")]
        public string UserId { get; set; } // Ma nguoi dung
        [Required(ErrorMessage = "Số dư là bắt buộc nhập!")]
        public decimal SoDu { get; set; } // So du trong vi tien
    }
}
