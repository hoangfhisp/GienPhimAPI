namespace GienPhim_Services.WalletService.Application.DTOs.Payments
{
    public class PaymentRequestDTO
    {
        public decimal SoTien { get; set; }
        public string MoTaDH { get; set; }
        public string KieuDH { get; set; }
        public string UserId { get; set; }
    }
}
