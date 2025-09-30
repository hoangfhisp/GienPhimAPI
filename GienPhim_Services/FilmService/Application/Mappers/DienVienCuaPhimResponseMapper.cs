using GienPhim_Services.FilmService.Application.DTOs.Client.DienVienCuaPhim;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class DienVienCuaPhimResponseMapper : IDienVienCuaPhimResponseMapper
    {
        public DienVienCuaPhimResponseDTO ToDto(DienVienCuaPhim entity)
        {
            return new DienVienCuaPhimResponseDTO
            {
                PhimId = entity.PhimId,
                TenPhim = entity.Phim != null ? entity.Phim.TenP : string.Empty,
                DienVienId = entity.DienVienId,
                TenDienVien = entity.DienVien != null ? entity.DienVien.TenDV : string.Empty,
                ThoiGianTao = entity.ThoiGianTao,
                ThoiGianCapNhat = entity.ThoiGianCapNhat
            };
        }

        public List<DienVienCuaPhimResponseDTO> ToDtoList(List<DienVienCuaPhim> entities)
        {
            return entities.Select(ToDto).ToList();
        }

        public UpdateDienVienCuaPhimResponseDTO ToUpdateDto(DienVienCuaPhim entity)
        {
            return new UpdateDienVienCuaPhimResponseDTO
            {
                PhimId = entity.PhimId,
                TenPhim = entity.Phim != null ? entity.Phim.TenP : string.Empty,
                DienVienId = entity.DienVienId,
                TenDienVien = entity.DienVien != null ? entity.DienVien.TenDV : string.Empty,
                ThoiGianTao = entity.ThoiGianTao,
                ThoiGianCapNhat = entity.ThoiGianCapNhat
            };
        }

        public List<UpdateDienVienCuaPhimResponseDTO> ToUpdateDtoList(List<DienVienCuaPhim> entities)
        {
            return entities.Select(ToUpdateDto).ToList();
        }
    }
}
