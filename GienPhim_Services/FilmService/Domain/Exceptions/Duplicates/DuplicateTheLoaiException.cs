using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Duplicates
{
    public class DuplicateTheLoaiException : DomainException
    {
        public DuplicateTheLoaiException(string tenTheLoai)
            : base($"Thể loại '{tenTheLoai}' đã tồn tại.") { }
    }
}
