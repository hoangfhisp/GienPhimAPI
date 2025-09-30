using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.NotFounds
{
    public class DienVienNotFoundException : DomainException
    {
        public DienVienNotFoundException(int dienVienId)
            : base($"Không tìm thấy diễn viên với Id = {dienVienId}.") { }
    }
}
