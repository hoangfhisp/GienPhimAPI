using GienPhim_Services.WalletService.Application.DTOs.Payments;
using GienPhim_Services.WalletService.Application.Interfaces.Payments;
using Microsoft.Extensions.Options;

namespace GienPhim_Services.WalletService.Infrastructure.Payments.VnPay
{
    public class VnPayPaymentGateway : IPaymentGateway
    {
        private readonly VnPaySettings _settings;

        public VnPayPaymentGateway(IOptions<VnPaySettings> settings)
        {
            _settings = settings.Value;
        }

        public Task<string> CreatePaymentUrl(
            PaymentRequestDTO model,
            string ipAddress,
            string orderId)
        {
            var vnpay = new VnPayClient();

            vnpay.AddRequestData("vnp_Version", VnPayClient.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", _settings.TmnCode);
            vnpay.AddRequestData("vnp_Amount", (model.SoTien * 100).ToString());
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_TxnRef", orderId);
            vnpay.AddRequestData("vnp_OrderInfo", model.MoTaDH);
            vnpay.AddRequestData("vnp_OrderType", model.KieuDH ?? "topup");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_IpAddr", ipAddress);
            vnpay.AddRequestData("vnp_ReturnUrl", _settings.ReturnUrl);

            var url = vnpay.CreateRequestUrl(
                _settings.Url,
                _settings.HashSecret
            );

            return Task.FromResult(url);
        }

        public Task<PaymentCallbackResultDTO> HandleCallback(IQueryCollection query)
        {
            var responseCode = query["vnp_ResponseCode"];
            var status = query["vnp_TransactionStatus"];
            var amount = decimal.Parse(query["vnp_Amount"]) / 100;

            return Task.FromResult(new PaymentCallbackResultDTO
            {
                IsSuccess = responseCode == "00" && status == "00",
                OrderId = query["vnp_TxnRef"],
                ResponseCode = responseCode,
                TransactionStatus = status,
                Amount = amount
            });
        }
    }
}
