using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Invalid
{
    public class InvalidDienVienException : DomainException
    {
        public InvalidDienVienException(string reason)
            : base($"Diễn viên không hợp lệ: {reason}.") { }
    }
}
