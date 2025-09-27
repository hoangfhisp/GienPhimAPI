using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Duplicates
{
    public class DuplicateTheTagException : DomainException
    {
        public DuplicateTheTagException(string tenTheTag)
            : base($"Thẻ tag '{tenTheTag}' đã tồn tại.") { }
    }
}
