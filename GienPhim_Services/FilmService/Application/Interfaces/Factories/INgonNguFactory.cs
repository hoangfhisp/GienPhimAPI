using GienPhim_Services.FilmService.Application.DTOs.Client.NgonNgu;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface INgonNguFactory
    {
        NgonNgu CreateNgonNgu(NgonNguCreateDTO dto);
        NgonNgu UpdateNgonNgu(NgonNguUpdateDTO dto);
        NgonNgu DeleteNgonNgu(NgonNguDeleteDTO dto);
    }
}
