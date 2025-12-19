using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Invalid
{
    public class InvalidNgonNguException : DomainException
    {
        public InvalidNgonNguException(string reason)
            : base($"Ngôn ngữ không hợp lệ: {reason}.") { }
    }
}
