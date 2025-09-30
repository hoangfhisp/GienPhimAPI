using GienPhim_Services.FilmService.Application.DTOs.Client.QuocGia;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface IQuocGiaFactory
    {
        QuocGia CreateQuocGia(QuocGiaCreateDTO dto);
        QuocGia UpdateQuocGia(QuocGiaUpdateDTO dto);
        QuocGia DeleteQuocGia(QuocGiaDeleteDTO dto);
    }
}
