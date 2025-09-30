using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.NotFounds
{
    public class TheTagNotFoundException : DomainException
    {
        public TheTagNotFoundException(int theTagId)
            : base($"Không tìm thấy thẻ tag với Id = {theTagId}.") { }
    }
}
