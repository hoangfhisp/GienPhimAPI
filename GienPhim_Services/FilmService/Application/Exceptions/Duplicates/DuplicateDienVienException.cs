using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Duplicates
{
    public class DuplicateDienVienException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateDienVienException(string ten)
            : base($"Tên diễn viên '{ten}' đã tồn tại.") { }
    }
}
