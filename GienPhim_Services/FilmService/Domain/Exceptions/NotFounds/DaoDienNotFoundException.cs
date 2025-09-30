using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.NotFounds
{
    public class DaoDienNotFoundException : DomainException
    {
        public DaoDienNotFoundException(int daoDienId)
            : base($"Không tìm thấy đạo diễn với Id = {daoDienId}.") { }
    }
}
