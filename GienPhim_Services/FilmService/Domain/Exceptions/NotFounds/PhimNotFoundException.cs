using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.NotFounds
{
    public class PhimNotFoundException : DomainException
    {
        public PhimNotFoundException(int phimId)
            : base($"Không tìm thấy phim với Id = {phimId}.") { }
    }
}
