using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Invalid
{
    public class InvalidLichXuXemException : DomainException
    {
        public InvalidLichXuXemException(string reason)
            : base($"Dữ liệu lịch sử xem không hợp lệ: {reason}") { }
    }
}
