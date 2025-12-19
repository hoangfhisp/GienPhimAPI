using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class QuocGiaNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public QuocGiaNotFoundException(string? quocGia = "")
            : base($"Quốc gia {quocGia} không tồn tại") { }
    }
}
