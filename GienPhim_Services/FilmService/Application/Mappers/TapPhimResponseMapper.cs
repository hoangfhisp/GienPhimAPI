using GienPhim_Services.FilmService.Application.DTOs.Client.TapPhim;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class TapPhimResponseMapper : ITapPhimResponseMapper
    {
        public TapPhimResponseDTO ToDto(TapPhim tapPhim)
        {
            return new TapPhimResponseDTO
            {
                TapId = tapPhim.TapId,
                PhimId = tapPhim.PhimId,
                TenPhim = tapPhim.Phim != null ? tapPhim.Phim.TenP : string.Empty,
                TenTap = tapPhim.TenTap,
                SlugTap = tapPhim.SlugTap,
                TenFile = tapPhim.TenFile,
                LinkChinh = tapPhim.LinkChinh,
                LinkPhu = tapPhim.LinkPhu
            };
        }

        public List<TapPhimResponseDTO> ToDtoList(List<TapPhim> tapPhims)
        {
            return tapPhims.Select(ToDto).ToList();
        }

        public UpdateTapPhimResponseDTO ToUpdateDto(TapPhim tapPhim)
        {
            return new UpdateTapPhimResponseDTO
            {
                TapId = tapPhim.TapId,
                PhimId = tapPhim.PhimId,
                TenPhim = tapPhim.Phim != null ? tapPhim.Phim.TenP : string.Empty,
                TenTap = tapPhim.TenTap,
                SlugTap = tapPhim.SlugTap,
                TenFile = tapPhim.TenFile,
                LinkChinh = tapPhim.LinkChinh,
                LinkPhu = tapPhim.LinkPhu,
                ThoiGianTao = tapPhim.ThoiGianTao,
                ThoiGianCapNhat = tapPhim.ThoiGianCapNhat
            };
        }

        public List<UpdateTapPhimResponseDTO> ToUpdateDtoList(List<TapPhim> tapPhims)
        {
            return tapPhims.Select(ToUpdateDto).ToList();
        }
    }
}
