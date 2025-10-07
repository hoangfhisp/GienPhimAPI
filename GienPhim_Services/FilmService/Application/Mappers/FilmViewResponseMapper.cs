using GienPhim_Services.FilmService.Application.DTOs.Client.FilmView;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class FilmViewResponseMapper : IFilmViewResponseMapper
    {
        public FilmViewResponseDTO ToDto(FilmView filmView)
        {
            return new FilmViewResponseDTO
            {
                FilmViewId = filmView.FilmViewId,
                PhimId = filmView.PhimId,
                TenPhim = filmView.Phim != null ? filmView.Phim.TenP : string.Empty,
                UserId = filmView.UserId,
                ThoiGianXem = filmView.ThoiGianXem
            };
        }

        public List<FilmViewResponseDTO> ToDtoList(List<FilmView> filmViews)
        {
            return filmViews.Select(ToDto).ToList();
        }

        public UpdateFilmViewResponseDTO ToUpdateDto(FilmView filmView)
        {
            return new UpdateFilmViewResponseDTO
            {
                FilmViewId = filmView.FilmViewId,
                PhimId = filmView.PhimId,
                TenPhim = filmView.Phim != null ? filmView.Phim.TenP : string.Empty,
                UserId = filmView.UserId,
                ThoiGianXem = filmView.ThoiGianXem
            };
        }

        public List<UpdateFilmViewResponseDTO> ToUpdateDtoList(List<FilmView> filmViews)
        {
            return filmViews.Select(ToUpdateDto).ToList();
        }
    }
}