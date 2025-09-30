using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDienCuaPhim;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class DaoDienCuaPhimFactory : IDaoDienCuaPhimFactory
    {
        public DaoDienCuaPhim Create(DaoDienCuaPhimCreateDTO dto)
        {
            return new DaoDienCuaPhim
            {
                PhimId = dto.PhimId,
                DaoDienId = dto.DaoDienId,
                VaiTroDG = dto.VaiTroDG?.Trim() ?? string.Empty,
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public DaoDienCuaPhim Update(DaoDienCuaPhimUpdateDTO dto)
        {
            return new DaoDienCuaPhim
            {
                PhimId = dto.PhimId,
                DaoDienId = dto.DaoDienId,
                VaiTroDG = dto.VaiTroDG?.Trim() ?? string.Empty,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public DaoDienCuaPhim Delete(DaoDienCuaPhimDeleteDTO dto)
        {
            return new DaoDienCuaPhim
            {
                PhimId = dto.PhimId,
                DaoDienId = dto.DaoDienId
            };
        }
    }
}
