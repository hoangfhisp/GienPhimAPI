using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Domain.Exceptions.Duplicates
{
    public class DuplicateNgonNguException : DomainException
    {
        public DuplicateNgonNguException(string tenNgonNgu)
            : base($"Ngôn ngữ '{tenNgonNgu}' đã tồn tại.") { }
    }
}
