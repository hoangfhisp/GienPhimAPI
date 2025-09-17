using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.NotFounds
{
    public class DaoDienNotFoundException : BusinessException
    {
        public override int StatusCode => 404; 

        public DaoDienNotFoundException(string? daoDien = "") : base($"Đạo Diễn {daoDien} không tồn tại") { }
    }
}
