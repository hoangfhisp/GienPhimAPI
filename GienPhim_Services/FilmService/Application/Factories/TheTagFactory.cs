using GienPhim_Services.FilmService.Application.DTOs.Client.TheTag;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class TheTagFactory : ITheTagFactory
    {
        public TheTag CreateTheTag(TheTagCreateDTO dto)
        {
            return new TheTag
            {
                TenT = dto.TenT?.Trim() ?? string.Empty,
                MoTaT = dto.MoTaT?.Trim(),
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public TheTag UpdateTheTag(TheTagUpdateDTO dto)
        {
            var now = DateTime.UtcNow;

            return new TheTag
            {
                TheTagId = dto.TheTagId,
                TenT = (dto.TenT ?? string.Empty).Trim(),
                MoTaT = string.IsNullOrWhiteSpace(dto.MoTaT) ? null : dto.MoTaT.Trim(),
                ThoiGianCapNhat = now
            };
        }

        public TheTag DeleteTheTag(TheTagDeleteDTO dto)
        {
            return new TheTag
            {
                TheTagId = dto.TheTagId
            };
        }
    }
}
