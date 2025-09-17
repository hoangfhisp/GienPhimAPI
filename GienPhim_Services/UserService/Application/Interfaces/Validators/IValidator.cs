namespace GienPhim_Services.UserService.Application.Interfaces.Validators
{
    public interface IValidator<T>
    {
        Task ValidateAsync(T dto, CancellationToken cancellationToken = default);
    }
}
