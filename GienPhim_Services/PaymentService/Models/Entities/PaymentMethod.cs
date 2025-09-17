using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.PaymentService.Models.Entities
{
    public class PaymentMethod
    {
        [Key]
        public string PaymentMethodId { get; set; } // Mã phương thức thanh toán
        public string UserId { get; set; } // Mã người dùng
        public string LoaiPhuongThuc { get; set; } // Loại phương thức thanh toán (Ví dụ: Thẻ tín dụng, Ví điện tử, Chuyển khoản ngân hàng)
        public string NhaCungCap { get; set; } // Nhà cung cấp dịch vụ thanh toán (Ví dụ: Stripe, PayPal, Ngân hàng)
        public string SoTaiKhoanAn { get; set; } // Số tài khoản (nếu là chuyển khoản ngân hàng)
        public int BonSoCuoi { get; set; } // 4 số cuối của thẻ tín dụng (nếu là thẻ tín dụng)
        public int ThangHetHan { get; set; } // Tháng hết hạn (nếu là thẻ tín dụng)
        public int NamHetHan { get; set; } // Năm hết hạn (nếu là thẻ tín dụng)
        public string Token { get; set; } // mã token hoá (khi kết nối với cổng thanh toán như MoMo, Stripe, VNPAY, PayPal)
        public string DiaChiThanhToan { get; set; } // Địa chỉ thanh toán (nếu cần thiết, ví dụ: đối với thẻ tín dụng hoặc ví điện tử)
        public bool PhuongThucMacDinh { get; set; } // Phương thức thanh toán mặc định (True/False)
        public DateTime NgayTao { get; set; } // Ngày tạo phương thức thanh toán
        public DateTime NgayCapNhat { get; set; } // Ngày cập nhật phương thức thanh toán

        public List<PaymentTransaction> PaymentTransactions { get; set; } // Danh sách giao dịch thanh toán liên quan
    }
}
