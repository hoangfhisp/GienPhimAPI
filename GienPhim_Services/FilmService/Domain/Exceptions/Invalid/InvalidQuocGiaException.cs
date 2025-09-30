using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Invalid
{
    public class InvalidQuocGiaException : DomainException
    {
        public InvalidQuocGiaException(string reason)
            : base($"Quốc gia không hợp lệ: {reason}.") { }
    }
}
