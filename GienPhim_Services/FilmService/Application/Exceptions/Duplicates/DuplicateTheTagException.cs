using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Duplicates
{
    public class DuplicateTheTagException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateTheTagException(string ten)
            : base($"Tên thẻ tag '{ten}' đã tồn tại.") { }
    }
}
