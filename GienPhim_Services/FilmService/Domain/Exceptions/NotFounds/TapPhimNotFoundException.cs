using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.NotFounds
{
    public class TapPhimNotFoundException : DomainException
    {
        public TapPhimNotFoundException(string tapId)
            : base($"Không tìm thấy tập phim với TapId = {tapId}.") { }
    }
}