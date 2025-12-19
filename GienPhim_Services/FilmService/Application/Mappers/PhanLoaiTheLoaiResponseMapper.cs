using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheLoai;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class PhanLoaiTheLoaiResponseMapper : IPhanLoaiTheLoaiResponseMapper
    {
        public PhanLoaiTheLoaiResponseDTO ToDto(PhanLoaiTheLoai phanLoaiTheLoai)
        {
            return new PhanLoaiTheLoaiResponseDTO
            {
                PhimId = phanLoaiTheLoai.PhimId,
                TenPhim = phanLoaiTheLoai.Phim != null ? phanLoaiTheLoai.Phim.TenP : string.Empty,
                TheLoaiId = phanLoaiTheLoai.TheLoaiId,
                TenTheLoai = phanLoaiTheLoai.TheLoai != null ? phanLoaiTheLoai.TheLoai.TenTL : string.Empty
            };
        }

        public List<PhanLoaiTheLoaiResponseDTO> ToDtoList(List<PhanLoaiTheLoai> phanLoaiTheLoais)
        {
            return phanLoaiTheLoais.Select(ToDto).ToList();
        }

        public UpdatePhanLoaiTheLoaiResponseDTO ToUpdateDto(PhanLoaiTheLoai phanLoaiTheLoai)
        {
            return new UpdatePhanLoaiTheLoaiResponseDTO
            {
                PhimId = phanLoaiTheLoai.PhimId,
                TenPhim = phanLoaiTheLoai.Phim != null ? phanLoaiTheLoai.Phim.TenP : string.Empty,
                TheLoaiId = phanLoaiTheLoai.TheLoaiId,
                TenTheLoai = phanLoaiTheLoai.TheLoai != null ? phanLoaiTheLoai.TheLoai.TenTL : string.Empty,
                ThoiGianTao = phanLoaiTheLoai.ThoiGianTao,
                ThoiGianCapNhat = phanLoaiTheLoai.ThoiGianCapNhat
            };
        }

        public List<UpdatePhanLoaiTheLoaiResponseDTO> ToUpdateDtoList(List<PhanLoaiTheLoai> phanLoaiTheLoais)
        {
            return phanLoaiTheLoais.Select(ToUpdateDto).ToList();
        }
    }
}
