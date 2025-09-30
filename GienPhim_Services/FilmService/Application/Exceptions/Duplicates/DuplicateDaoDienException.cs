using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Duplicates
{
    public class DuplicateDaoDienException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateDaoDienException(string ten)
            : base($"Tên đạo diễn '{ten}' đã tồn tại.") { }
    }
}
