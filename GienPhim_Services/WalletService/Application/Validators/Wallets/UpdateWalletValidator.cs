using GienPhim_Services.WalletService.Application.DTOs.Wallets;
using GienPhim_Services.WalletService.Application.Exceptions.NotFounds;
using GienPhim_Services.WalletService.Application.Interfaces.Repositories;
using GienPhim_Services.WalletService.Application.Interfaces.Validators;

namespace GienPhim_Services.WalletService.Application.Validators.Wallets
{
    public class UpdateWalletValidator : IValidator<UpdateWalletRequestDTO>
    {
        private readonly IWalletRepository _walletRepository;
        public UpdateWalletValidator(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task ValidateAsync(UpdateWalletRequestDTO dto, CancellationToken cancellationToken = default)
        {
            var existingWallet = await _walletRepository.FindWalletByWalletIdAsync(dto.WalletId, cancellationToken);
            if (existingWallet == null)
            {
                throw new WalletNotFoundException(dto.WalletId);
            }

        }
    }
}
