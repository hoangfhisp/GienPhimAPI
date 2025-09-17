using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GienPhim_Services.OrderService.Models.Entities
{
    public class OrderDetail
    {
        [Key]
        public string OrderDetailId { get; set; } // Mã chi tiết đơn hàng
        public string OrderId { get; set; } // Mã đơn hàng
        [ForeignKey("OrderId")]
        public Order Order { get; set; } // Thông tin đơn hàng liên quan
        public string ProductName { get; set; } // Mã sản phẩm
        public int SoLuong { get; set; } // Số lượng sản phẩm trong chi tiết đơn hàng
        public decimal DonGia { get; set; } // Đơn giá của sản phẩm
        public decimal ThanhTien { get; set; } // Thành tiền (SoLuong * DonGia)

    }
}
