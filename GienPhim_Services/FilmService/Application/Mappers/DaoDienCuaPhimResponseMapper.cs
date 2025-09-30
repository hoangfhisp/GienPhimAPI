using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDienCuaPhim;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class DaoDienCuaPhimResponseMapper : IDaoDienCuaPhimResponseMapper
    {
        public DaoDienCuaPhimResponseDTO ToDto(DaoDienCuaPhim entity)
        {
            return new DaoDienCuaPhimResponseDTO
            {
                PhimId = entity.PhimId,
                TenPhim = entity.Phim != null ? entity.Phim.TenP : string.Empty,
                DaoDienId = entity.DaoDienId,
                TenDaoDien = entity.DaoDien != null ? entity.DaoDien.TenDD : string.Empty,
                VaiTroDG = entity.VaiTroDG,
                ThoiGianTao = entity.ThoiGianTao,
                ThoiGianCapNhat = entity.ThoiGianCapNhat
            };
        }

        public List<DaoDienCuaPhimResponseDTO> ToDtoList(List<DaoDienCuaPhim> entities)
        {
            return entities.Select(e => ToDto(e)).ToList();
        }

        public UpdateDaoDienCuaPhimResponseDTO ToUpdateDto(DaoDienCuaPhim entity)
        {
            return new UpdateDaoDienCuaPhimResponseDTO
            {
                PhimId = entity.PhimId,
                TenPhim = entity.Phim != null ? entity.Phim.TenP : string.Empty,
                DaoDienId = entity.DaoDienId,
                TenDaoDien = entity.DaoDien != null ? entity.DaoDien.TenDD : string.Empty,
                VaiTroDG = entity.VaiTroDG,
                ThoiGianTao = entity.ThoiGianTao,
                ThoiGianCapNhat = entity.ThoiGianCapNhat
            };
        }

        public List<UpdateDaoDienCuaPhimResponseDTO> ToUpdateDtoList(List<DaoDienCuaPhim> entities)
        {
            return entities.Select(e => ToUpdateDto(e)).ToList();
        }
    }
}
