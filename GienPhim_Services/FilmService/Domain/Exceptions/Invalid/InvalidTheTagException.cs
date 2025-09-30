using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Invalid
{
    public class InvalidTheTagException : DomainException
    {
        public InvalidTheTagException(string reason)
            : base($"Thẻ tag không hợp lệ: {reason}.") { }
    }
}
