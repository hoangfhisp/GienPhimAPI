using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheTag;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class PhanLoaiTheTagResponseMapper : IPhanLoaiTheTagResponseMapper
    {
        public PhanLoaiTheTagResponseDTO ToDto(PhanLoaiTheTag phanLoaiTheTag)
        {
            return new PhanLoaiTheTagResponseDTO
            {
                PhimId = phanLoaiTheTag.PhimId,
                TenPhim = phanLoaiTheTag.Phim != null ? phanLoaiTheTag.Phim.TenP : string.Empty,
                TheTagId = phanLoaiTheTag.TheTagId,
                TenTheTag = phanLoaiTheTag.TheTag != null ? phanLoaiTheTag.TheTag.TenT : string.Empty
            };
        }

        public List<PhanLoaiTheTagResponseDTO> ToDtoList(List<PhanLoaiTheTag> phanLoaiTheTags)
        {
            return phanLoaiTheTags.Select(ToDto).ToList();
        }

        public UpdatePhanLoaiTheTagResponseDTO ToUpdateDto(PhanLoaiTheTag phanLoaiTheTag)
        {
            return new UpdatePhanLoaiTheTagResponseDTO
            {
                PhimId = phanLoaiTheTag.PhimId,
                TenPhim = phanLoaiTheTag.Phim != null ? phanLoaiTheTag.Phim.TenP : string.Empty,
                TheTagId = phanLoaiTheTag.TheTagId,
                TenTheTag = phanLoaiTheTag.TheTag != null ? phanLoaiTheTag.TheTag.TenT : string.Empty,
                ThoiGianTao = phanLoaiTheTag.ThoiGianTao,
                ThoiGianCapNhat = phanLoaiTheTag.ThoiGianCapNhat
            };
        }

        public List<UpdatePhanLoaiTheTagResponseDTO> ToUpdateDtoList(List<PhanLoaiTheTag> phanLoaiTheTags)
        {
            return phanLoaiTheTags.Select(ToUpdateDto).ToList();
        }
    }
}
