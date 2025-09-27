using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Duplicates
{
    public class DuplicateDienVienException : DomainException
    {
        public DuplicateDienVienException(string tenDienVien)
            : base($"Diễn viên '{tenDienVien}' đã tồn tại.") { }
    }
}
