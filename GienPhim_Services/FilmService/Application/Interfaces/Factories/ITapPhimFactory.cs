using GienPhim_Services.FilmService.Application.DTOs.Client.TapPhim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface ITapPhimFactory
    {
        TapPhim Create(TapPhimCreateDTO dto, string? linkChinhFileName);
        TapPhim Update(TapPhimUpdateDTO dto, string? linkChinhFileName);
        TapPhim Delete(TapPhimDeleteDTO dto);
    }
}
