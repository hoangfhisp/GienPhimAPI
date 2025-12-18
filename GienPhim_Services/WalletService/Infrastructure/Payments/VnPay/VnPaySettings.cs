namespace GienPhim_Services.WalletService.Infrastructure.Payments.VnPay
{
    public class VnPaySettings
    {
        public string Url { get; set; }
        public string ReturnUrl { get; set; }
        public string TmnCode { get; set; }
        public string HashSecret { get; set; }
    }
}
