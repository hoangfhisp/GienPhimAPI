using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Duplicates
{
    // Ném ra khi phát hiện đạo diễn bị trùng lặp (ví dụ: tên đã tồn tại).
    public class DuplicateDaoDienException : BusinessException
    {
        public override int StatusCode => 409;

        public DuplicateDaoDienException(string tenDaoDien)
            : base($"Đạo diễn '{tenDaoDien}' đã tồn tại.") { }
    }
}
