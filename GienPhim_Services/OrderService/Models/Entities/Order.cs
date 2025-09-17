using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.OrderService.Models.Entities
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; } // Mã đơn hàng
        public string UserId { get; set; } // Mã người dùng
        public decimal TongTien { get; set; } // Tổng số tiền của đơn hàng
        public string TrangThai { get; set; } // Trạng thái đơn hàng (Chờ xử lý, Đang mua, Đã hủy)
        public DateTime NgayTao { get; set; } // Ngày tạo đơn hàng
        public DateTime NgayCapNhat { get; set; } // Ngày cập nhật đơn hàng
        // Thông tin chi tiết đơn hàng liên quan
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
