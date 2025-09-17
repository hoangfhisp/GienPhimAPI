using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GienPhim_Services.PaymentService.Models.Entities
{
    public class PaymentTransaction
    {
        [Key]
        public string PaymentTransactionId { get; set; } // Mã giao dịch thanh toán
        public string UserId { get; set; } // Mã người dùng
        public string PaymentMethodId { get; set; } // Mã phương thức thanh toán
        [ForeignKey("PaymentMethodId")]
        public PaymentMethod PaymentMethod { get; set; } // Thông tin phương thức thanh toán liên quan
        public decimal SoTien { get; set; } // Số tiền giao dịch
        public string TrangThai { get; set; } // Trạng thái giao dịch (Thành công, Thất bại, Đang xử lý)
        public string MaGiaoDich { get; set; } // Mã giao dịch từ cổng thanh toán (ví dụ: mã giao dịch MoMo, Stripe, PayPal)
        public DateTime NgayTao { get; set; } // Ngày tạo giao dich
        public DateTime NgayCapNhat { get; set; } // Ngày cập nhật giao dich


    }
}
