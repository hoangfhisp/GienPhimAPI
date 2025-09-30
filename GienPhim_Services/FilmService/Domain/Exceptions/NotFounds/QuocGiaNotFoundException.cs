using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.NotFounds
{
    public class QuocGiaNotFoundException : DomainException
    {
        public QuocGiaNotFoundException(int quocGiaId)
            : base($"Không tìm thấy quốc gia với Id = {quocGiaId}.") { }
    }
}
