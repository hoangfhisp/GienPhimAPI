using GienPhim_Services.WalletService.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace GienPhim_Services.WalletService.Application.DTOs.Wallets
{
    public class WalletTransactionResponseDTO
    {
        public string WalletTransactionId { get; set; } // Mã giao dịch ví tiền
        public string LoaiGiaoDich { get; set; } // Loại giao dịch (nạp tiền, rút tiền, chuyển tiền, v.v.)
        public decimal SoTienThayDoi { get; set; } // Số tiền thay doi
        public decimal SoDuSauGiaoDich { get; set; } // Số dư sau giao dịch
        public string MoTa { get; set; } // Mô tả giao dịch
        public DateTime NgayGiaoDich { get; set; } // Ngày giao dịch
        public string TrangThai { get; set; } // Trạng thái giao dịch (thành công, thất bại, đang xử lý, v.v.)
    }
}
