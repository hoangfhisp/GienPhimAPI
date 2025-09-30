using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Duplicates
{
    public class DuplicateQuocGiaException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateQuocGiaException(string ten)
            : base($"Tên quốc gia '{ten}' đã tồn tại.") { }
    }
}
