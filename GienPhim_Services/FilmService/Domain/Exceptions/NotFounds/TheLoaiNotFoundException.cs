using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.NotFounds
{
    public class TheLoaiNotFoundException : DomainException
    {
        public TheLoaiNotFoundException(int theLoaiId)
            : base($"Không tìm thấy thể loại với Id = {theLoaiId}.") { }
    }
}
