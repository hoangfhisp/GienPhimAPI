using GienPhim_Services.InteractionService.Application.Interfaces.Services;

namespace GienPhim_Services.InteractionService.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
