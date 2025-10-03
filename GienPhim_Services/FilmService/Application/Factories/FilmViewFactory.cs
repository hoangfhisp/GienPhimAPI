using GienPhim_Services.FilmService.Application.DTOs.Client.FilmView;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class FilmViewFactory : IFilmViewFactory
    {
        public FilmView Create(FilmViewCreateDTO dto)
        {
            return new FilmView
            {
                FilmViewId = Guid.NewGuid().ToString("N"),
                PhimId = dto.PhimId,
                UserId = dto.UserId,
                ThoiGianXem = DateTime.UtcNow
            };
        }

        public FilmView Update(FilmViewUpdateDTO dto)
        {
            return new FilmView
            {
                FilmViewId = dto.FilmViewId,
                PhimId = dto.PhimId,
                UserId = dto.UserId,
                ThoiGianXem = dto.ThoiGianXem
            };
        }

        public FilmView Delete(FilmViewDeleteDTO dto)
        {
            return new FilmView
            {
                FilmViewId = dto.FilmViewId
            };
        }
    }
}
