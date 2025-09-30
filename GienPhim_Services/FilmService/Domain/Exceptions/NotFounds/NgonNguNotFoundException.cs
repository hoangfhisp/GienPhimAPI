using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.NotFounds
{
    public class NgonNguNotFoundException : DomainException
    {
        public NgonNguNotFoundException(int ngonNguId)
            : base($"Không tìm thấy ngôn ngữ với Id = {ngonNguId}.") { }
    }
}
