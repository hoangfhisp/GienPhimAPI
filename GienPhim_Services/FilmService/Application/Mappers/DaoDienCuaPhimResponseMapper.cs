using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDienCuaPhim;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class DaoDienCuaPhimResponseMapper : IDaoDienCuaPhimResponseMapper
    {
        public DaoDienCuaPhimResponseDTO ToDto(DaoDienCuaPhim daoDienCuaPhim)
        {
            return new DaoDienCuaPhimResponseDTO
            {
                PhimId = daoDienCuaPhim.PhimId,
                TenPhim = daoDienCuaPhim.Phim != null ? daoDienCuaPhim.Phim.TenP : string.Empty,
                DaoDienId = daoDienCuaPhim.DaoDienId,
                TenDaoDien = daoDienCuaPhim.DaoDien != null ? daoDienCuaPhim.DaoDien.TenDD : string.Empty,
                VaiTroDG = daoDienCuaPhim.VaiTroDG,
                ThoiGianTao = daoDienCuaPhim.ThoiGianTao,
                ThoiGianCapNhat = daoDienCuaPhim.ThoiGianCapNhat
            };
        }

        public List<DaoDienCuaPhimResponseDTO> ToDtoList(List<DaoDienCuaPhim> daoDienCuaPhims)
        {
            return daoDienCuaPhims.Select(e => ToDto(e)).ToList();
        }

        public UpdateDaoDienCuaPhimResponseDTO ToUpdateDto(DaoDienCuaPhim daoDienCuaPhim)
        {
            return new UpdateDaoDienCuaPhimResponseDTO
            {
                PhimId = daoDienCuaPhim.PhimId,
                TenPhim = daoDienCuaPhim.Phim != null ? daoDienCuaPhim.Phim.TenP : string.Empty,
                DaoDienId = daoDienCuaPhim.DaoDienId,
                TenDaoDien = daoDienCuaPhim.DaoDien != null ? daoDienCuaPhim.DaoDien.TenDD : string.Empty,
                VaiTroDG = daoDienCuaPhim.VaiTroDG,
                ThoiGianTao = daoDienCuaPhim.ThoiGianTao,
                ThoiGianCapNhat = daoDienCuaPhim.ThoiGianCapNhat
            };
        }

        public List<UpdateDaoDienCuaPhimResponseDTO> ToUpdateDtoList(List<DaoDienCuaPhim> daoDienCuaPhims)
        {
            return daoDienCuaPhims.Select(e => ToUpdateDto(e)).ToList();
        }
    }
}
