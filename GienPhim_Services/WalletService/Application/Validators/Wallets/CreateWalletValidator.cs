using GienPhim_Services.WalletService.Application.DTOs.Wallets;
using GienPhim_Services.WalletService.Application.Exceptions.Duplicates;
using GienPhim_Services.WalletService.Application.Interfaces.Repositories;
using GienPhim_Services.WalletService.Application.Interfaces.Validators;

namespace GienPhim_Services.WalletService.Application.Validators.Wallets
{
    public class CreateWalletValidator : IContextValidator<CreateWalletRequestDTO,string>
    {
        private readonly IWalletRepository _walletRepository;

        public CreateWalletValidator(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task ValidateAsync(CreateWalletRequestDTO dto, CancellationToken cancellationToken = default)
        {
        }

        public async Task ValidateAsync(CreateWalletRequestDTO dto,string userId, CancellationToken cancellationToken = default)
        {
            var existingWallet = await _walletRepository.FindWalletByUserId(userId, cancellationToken);

            if (existingWallet != null)
            {
                throw new DuplicateWalletUserIdException(userId);
            }
        }
    }
}
