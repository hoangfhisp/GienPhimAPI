namespace GienPhim_Services.WalletService.Application.Interfaces.Validators
{
    public interface IValidator<T>
    {
        Task ValidateAsync(T dto, CancellationToken cancellationToken = default);
    }
}
