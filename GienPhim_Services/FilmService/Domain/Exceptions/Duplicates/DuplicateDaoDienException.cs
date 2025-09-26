using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Duplicates
{
    public class DuplicateDaoDienException : DomainException
    {
        public DuplicateDaoDienException(string tenDaoDien)
            : base($"Đạo diễn '{tenDaoDien}' đã tồn tại.") { }
    }
}
