namespace GienPhim_Services.MembershipService.Application.DTOs.Http
{
    public class WalletHttpResponseDTO
    {
        public string WalletId { get; set; } // Ma ví tiền
        public decimal SoDu { get; set; } // So du trong vi tien
        public bool TrangThaiVi { get; set; } // Tong so tien nap vao vi tien
        public DateTime NgayTao { get; set; } // Ngay tao vi tien
        public DateTime NgayCapNhat { get; set; } // Ngay cap nhat vi tien
    }
}
