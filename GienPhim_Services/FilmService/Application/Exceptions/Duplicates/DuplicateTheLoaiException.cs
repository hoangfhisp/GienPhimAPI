using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Duplicates
{
    public class DuplicateTheLoaiException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateTheLoaiException(string ten)
            : base($"Tên thể loại '{ten}' đã tồn tại.") { }
    }
}
