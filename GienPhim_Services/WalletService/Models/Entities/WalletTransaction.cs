using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GienPhim_Services.WalletService.Models.Entities
{
    public class WalletTransaction
    {
        [Key]
        public string WalletTransactionId { get; set; } // Mã giao dịch ví tiền
        public string WalletId { get; set; } // Mã ví tiền
        [ForeignKey("WalletId")]
        public Wallet Wallet { get; set; } // Thông tin ví tiền liên kết
        public string LoaiGiaoDich { get; set; } // Loại giao dịch (nạp tiền, rút tiền, chuyển tiền, v.v.)
        public decimal SoTienThayDoi { get; set; } // Số tiền thay doi
        public decimal SoDuSauGiaoDich { get; set; } // Số dư sau giao dịch
        public string MoTa { get; set; } // Mô tả giao dịch
        public DateTime NgayGiaoDich { get; set; } // Ngày giao dịch
        public string TrangThai { get; set; } // Trạng thái giao dịch (thành công, thất bại, đang xử lý, v.v.)

    }
}
