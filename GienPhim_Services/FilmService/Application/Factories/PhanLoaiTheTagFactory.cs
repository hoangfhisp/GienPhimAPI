using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheTag;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class PhanLoaiTheTagFactory : IPhanLoaiTheTagFactory
    {
        public PhanLoaiTheTag Create(PhanLoaiTheTagCreateDTO dto)
        {
            return new PhanLoaiTheTag
            {
                PhimId = dto.PhimId,
                TheTagId = dto.TheTagId,
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public PhanLoaiTheTag Update(PhanLoaiTheTagUpdateDTO dto)
        {
            return new PhanLoaiTheTag
            {
                PhimId = dto.PhimId,
                TheTagId = dto.TheTagId,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public PhanLoaiTheTag Delete(PhanLoaiTheTagDeleteDTO dto)
        {
            return new PhanLoaiTheTag
            {
                PhimId = dto.PhimId,
                TheTagId = dto.TheTagId
            };
        }
    }
}
