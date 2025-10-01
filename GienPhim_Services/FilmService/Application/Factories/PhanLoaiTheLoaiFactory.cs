using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheLoai;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class PhanLoaiTheLoaiFactory : IPhanLoaiTheLoaiFactory
    {
        public PhanLoaiTheLoai Create(PhanLoaiTheLoaiCreateDTO dto)
        {
            return new PhanLoaiTheLoai
            {
                PhimId = dto.PhimId,
                TheLoaiId = dto.TheLoaiId,
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public PhanLoaiTheLoai Update(PhanLoaiTheLoaiUpdateDTO dto)
        {
            return new PhanLoaiTheLoai
            {
                PhimId = dto.PhimId,
                TheLoaiId = dto.TheLoaiId,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public PhanLoaiTheLoai Delete(PhanLoaiTheLoaiDeleteDTO dto)
        {
            return new PhanLoaiTheLoai
            {
                PhimId = dto.PhimId,
                TheLoaiId = dto.TheLoaiId
            };
        }
    }
}
