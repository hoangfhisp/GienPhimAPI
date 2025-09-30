using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Duplicates
{
    public class DuplicateQuocGiaException : DomainException
    {
        public DuplicateQuocGiaException(string tenQuocGia)
            : base($"Quốc gia '{tenQuocGia}' đã tồn tại.") { }
    }
}
