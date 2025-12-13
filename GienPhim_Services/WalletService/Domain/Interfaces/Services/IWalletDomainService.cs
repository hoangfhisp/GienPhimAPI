using GienPhim_Services.WalletService.Domain.Entities;

namespace GienPhim_Services.WalletService.Domain.Interfaces.Services
{
    public interface IWalletDomainService
    {
        void EnsureCanCreate(Wallet wallet); 
    }
}
