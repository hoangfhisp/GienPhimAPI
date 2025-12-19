namespace GienPhim_Services.WalletService.Application.DTOs.Payments
{
    public class PaymentCallbackResultDTO
    {
        public bool IsSuccess { get; set; }
        public string OrderId { get; set; }
        public string ResponseCode { get; set; }
        public string TransactionStatus { get; set; }
        public decimal Amount { get; set; }
    }
}
