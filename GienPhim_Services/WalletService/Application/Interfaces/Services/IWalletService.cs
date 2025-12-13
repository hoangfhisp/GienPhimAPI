using GienPhim_Services.WalletService.Application.DTOs.Common;
using GienPhim_Services.WalletService.Application.DTOs.Wallets;

namespace GienPhim_Services.WalletService.Application.Interfaces.Services
{
    public interface IWalletService
    {
        Task<ResultDTO<WalletResponseDTO>> CreateWalletAsync(CreateWalletRequestDTO dto, CancellationToken cancellationToken = default);
    }
}
