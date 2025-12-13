namespace GienPhim_Services.InteractionService.Application.Interfaces.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
