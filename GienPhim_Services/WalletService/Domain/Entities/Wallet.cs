using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.WalletService.Domain.Entities
{
    public class Wallet
    {
        [Key] 
        public string WalletId { get; set; } // Ma ví tiền
        public string UserId { get; set; } // Ma nguoi dung
        public decimal SoDu { get; set; } // So du trong vi tien
        public bool TrangThaiVi { get; set; } // Tong so tien nap vao vi tien
        public DateTime NgayTao { get; set; } // Ngay tao vi tien
        public DateTime NgayCapNhat { get; set; } // Ngay cap nhat vi tien

        public List<WalletTransaction> WalletTransactions { get; set; } // Danh sach giao dich lien quan den vi tien

        public bool IsActive()
        {
            return TrangThaiVi == true;
        }
    }
}
