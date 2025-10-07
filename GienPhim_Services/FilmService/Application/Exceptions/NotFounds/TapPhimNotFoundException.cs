using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class TapPhimNotFoundException : BusinessException
    {
        public override int StatusCode => 404;

        public TapPhimNotFoundException()
            : base("Không tìm thấy tập phim nào.") { }

        public TapPhimNotFoundException(string tapId)
            : base($"Không tìm thấy tập phim với Id = {tapId}") { }

        public TapPhimNotFoundException(int phimId)
        : base($"Không tìm thấy tập phim nào của PhimId = {phimId}") { }
    }
}
