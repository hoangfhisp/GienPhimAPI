using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class TheLoaiNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public TheLoaiNotFoundException(string? theLoai = "")
            : base($"Thể Loại {theLoai} không tồn tại") { }
    }
}
