using GienPhim_Services.WalletService.Application.DTOs.Payments;

namespace GienPhim_Services.WalletService.Application.Interfaces.Payments
{
    public interface IPaymentGateway
    {
        Task<string> CreatePaymentUrl(
            PaymentRequestDTO model,
            string ipAddress,
            string orderId
        );

        Task<PaymentCallbackResultDTO> HandleCallback(IQueryCollection query);
    }
}
