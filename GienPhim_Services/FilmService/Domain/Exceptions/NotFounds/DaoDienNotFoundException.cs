using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.NotFounds
{
    public class DaoDienNotFoundException : BusinessException
    {
        public override int StatusCode => 404; // Not Found

        public DaoDienNotFoundException(int id)
            : base($"Không tìm thấy đạo diễn với Id = {id}.") { }

        public DaoDienNotFoundException(string tenDaoDien)
            : base($"Không tìm thấy đạo diễn có tên '{tenDaoDien}'.") { }
    }
}
