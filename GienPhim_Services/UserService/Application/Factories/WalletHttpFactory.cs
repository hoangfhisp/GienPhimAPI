using GienPhim_Services.UserService.Application.DTOs.Http;
using GienPhim_Services.UserService.Application.Interfaces.Factories;

namespace GienPhim_Services.UserService.Application.Factories
{
    public class WalletHttpFactory : IWalletHttpFactory
    {
        // Tạo DTO để gọi API tạo ví
        public CreateWalletHttpRequestDTO toCreateWallet (string userId)
        {
            return new CreateWalletHttpRequestDTO
            {
                UserId = userId,
                SoDu = 0
            };
        } 
    }
}
