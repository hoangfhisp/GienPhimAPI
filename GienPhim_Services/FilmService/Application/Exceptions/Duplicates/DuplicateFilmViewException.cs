using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Duplicates
{
    public class DuplicateFilmViewException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateFilmViewException(int phimId, string? userId)
            : base($"Người dùng {(string.IsNullOrEmpty(userId) ? "ẩn danh" : userId)} đã có lượt xem cho phim Id={phimId}.") { }
    }
}
