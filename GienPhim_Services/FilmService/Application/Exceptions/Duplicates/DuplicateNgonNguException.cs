using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Duplicates
{
    public class DuplicateNgonNguException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateNgonNguException(string ten)
            : base($"Tên ngôn ngữ '{ten}' đã tồn tại.") { }
    }
}
