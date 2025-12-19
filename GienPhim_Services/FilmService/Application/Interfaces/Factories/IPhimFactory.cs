using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface IPhimFactory
    {
        Phim CreatePhim(PhimCreateDTO dto, string avatar, string poster);
        Phim UpdatePhim(PhimUpdateDTO dto, string avatar, string poster);
        Phim DeletePhim(PhimDeleteDTO dto);
    }
}
