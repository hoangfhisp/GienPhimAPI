using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class PhimNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public PhimNotFoundException()
            : base("Không tìm thấy phim nào") { }

        public PhimNotFoundException(int phimId)
            : base($"Không tìm thấy Phim với Id = {phimId}") { }
    }
}
