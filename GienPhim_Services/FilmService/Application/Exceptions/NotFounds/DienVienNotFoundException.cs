using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class DienVienNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public DienVienNotFoundException(string? dienVien = "")
            : base($"Diễn Viên {dienVien} không tồn tại") { }
    }
}
