using GienPhim_Services.FilmService.Application.DTOs.Client.DienVienCuaPhim;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class DienVienCuaPhimResponseMapper : IDienVienCuaPhimResponseMapper
    {
        public DienVienCuaPhimResponseDTO ToDto(DienVienCuaPhim dienVienCuaPhim)
        {
            return new DienVienCuaPhimResponseDTO
            {
                PhimId = dienVienCuaPhim.PhimId,
                TenPhim = dienVienCuaPhim.Phim != null ? dienVienCuaPhim.Phim.TenP : string.Empty,
                DienVienId = dienVienCuaPhim.DienVienId,
                TenDienVien = dienVienCuaPhim.DienVien != null ? dienVienCuaPhim.DienVien.TenDV : string.Empty,
                ThoiGianTao = dienVienCuaPhim.ThoiGianTao,
                ThoiGianCapNhat = dienVienCuaPhim.ThoiGianCapNhat
            };
        }

        public List<DienVienCuaPhimResponseDTO> ToDtoList(List<DienVienCuaPhim> dienVienCuaPhims)
        {
            return dienVienCuaPhims.Select(ToDto).ToList();
        }

        public UpdateDienVienCuaPhimResponseDTO ToUpdateDto(DienVienCuaPhim dienVienCuaPhim)
        {
            return new UpdateDienVienCuaPhimResponseDTO
            {
                PhimId = dienVienCuaPhim.PhimId,
                TenPhim = dienVienCuaPhim.Phim != null ? dienVienCuaPhim.Phim.TenP : string.Empty,
                DienVienId = dienVienCuaPhim.DienVienId,
                TenDienVien = dienVienCuaPhim.DienVien != null ? dienVienCuaPhim.DienVien.TenDV : string.Empty,
                ThoiGianTao = dienVienCuaPhim.ThoiGianTao,
                ThoiGianCapNhat = dienVienCuaPhim.ThoiGianCapNhat
            };
        }

        public List<UpdateDienVienCuaPhimResponseDTO> ToUpdateDtoList(List<DienVienCuaPhim> dienVienCuaPhims)
        {
            return dienVienCuaPhims.Select(ToUpdateDto).ToList();
        }
    }
}
