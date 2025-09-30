using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Invalid
{
    public class InvalidDaoDienException : DomainException
    {
        public InvalidDaoDienException(string reason)
            : base($"Đạo diễn không hợp lệ: {reason}.") { }
    }
}
