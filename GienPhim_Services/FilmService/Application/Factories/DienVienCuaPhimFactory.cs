using GienPhim_Services.FilmService.Application.DTOs.Client.DienVienCuaPhim;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class DienVienCuaPhimFactory : IDienVienCuaPhimFactory
    {
        public DienVienCuaPhim Create(DienVienCuaPhimCreateDTO dto)
        {
            return new DienVienCuaPhim
            {
                PhimId = dto.PhimId,
                DienVienId = dto.DienVienId,
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public DienVienCuaPhim Update(DienVienCuaPhimUpdateDTO dto)
        {
            return new DienVienCuaPhim
            {
                PhimId = dto.PhimId,
                DienVienId = dto.DienVienId,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public DienVienCuaPhim Delete(DienVienCuaPhimDeleteDTO dto)
        {
            return new DienVienCuaPhim
            {
                PhimId = dto.PhimId,
                DienVienId = dto.DienVienId
            };
        }
    }
}
