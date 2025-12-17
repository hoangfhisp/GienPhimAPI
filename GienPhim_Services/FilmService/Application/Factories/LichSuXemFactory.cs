using GienPhim_Services.FilmService.Application.DTOs.Client.LichSuXem;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class LichSuXemFactory : ILichSuXemFactory
    {
        public LichSuXem Create(LichSuXemCreateDTO dto, string tapPhimId)
        {
            return new LichSuXem
            {
                PhimId = dto.PhimId,
                UserId = dto.UserId.Trim(),
                TapPhimId = tapPhimId.Trim(),
                TapVuaXem = dto.TapVuaXem.Trim(),
                ThoiGianXem = DateTime.UtcNow,
            };
        }
    }
}
