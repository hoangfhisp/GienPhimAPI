using GienPhim_Services.FilmService.Application.DTOs.Client.FilmView;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Interfaces.Factories
{
    public interface IFilmViewFactory
    {
        FilmView Create(FilmViewCreateDTO dto);
        FilmView Update(FilmViewUpdateDTO dto);
        FilmView Delete(FilmViewDeleteDTO dto);
    }
}
