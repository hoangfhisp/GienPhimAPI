using GienPhim_Services.FilmService.Application.DTOs.Client.LichSuXem;
using GienPhim_Services.WalletService.Application.DTOs.Wallets;
using GienPhim_Services.WalletService.Application.Exceptions.Invalids;
using GienPhim_Services.WalletService.Application.Exceptions.NotFounds;
using GienPhim_Services.WalletService.Application.Interfaces.Repositories;
using GienPhim_Services.WalletService.Application.Interfaces.Validators;

namespace GienPhim_Services.WalletService.Application.Validators.Wallets
{
    public class CreateWalletTransactionValidator : IEntityValidator<CreateWalletTransactionRequestDTO,decimal>
    {
        private readonly IWalletRepository _walletRepository;
        public CreateWalletTransactionValidator(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task ValidateAsync(CreateWalletTransactionRequestDTO dto, CancellationToken cancellationToken = default)
        {

        }

        public async Task<decimal> ValidateAndGetAsync(CreateWalletTransactionRequestDTO dto, CancellationToken cancellationToken = default)
        {
            var existingWallet = await _walletRepository.FindWalletByWalletIdAsync(dto.WalletId, cancellationToken);
            if (existingWallet == null)
            {
                throw new WalletNotFoundException(dto.WalletId);
            }
            if (existingWallet.SoDu + dto.SoTienThayDoi < 0)
            {
                throw new InvalidWalletBalanceException(dto.WalletId);
            }

            var soDuSauGiaoDich = existingWallet.SoDu + dto.SoTienThayDoi;

            return soDuSauGiaoDich;
        }

    }
}
