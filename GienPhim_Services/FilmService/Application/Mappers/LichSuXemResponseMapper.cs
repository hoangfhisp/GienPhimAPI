using GienPhim_Services.FilmService.Application.DTOs.Client.LichSuXem;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class LichSuXemResponseMapper : ILichSuXemResponseMapper
    {
        public LichSuXemResponseDTO toDTO(LichSuXem lichSuXem,Phim phim)
        {
            return new LichSuXemResponseDTO
            {
                LichSuXemId = lichSuXem.LichSuXemId,
                UserId = lichSuXem.UserId,
                PhimId = lichSuXem.PhimId,
                TenP = phim.TenP,
                MoTaP = phim.MoTaP,
                AvatarP = phim.AvatarP,
                AvatarPoster = phim.AvatarPoster,
                TenCTSXP = phim.TenCTSXP,
                LuotXemP = phim.LuotXemP,
                SoTapP = phim.SoTapP,
                LuotDislikeP = phim.LuotDislikeP,
                LuotThichP = phim.LuotThichP,
                ThuoiLuongP = phim.ThuoiLuongP,
                TapPhimId = lichSuXem.TapPhimId,
                TapVuaXem = lichSuXem.TapVuaXem,
                ThoiGianXem = lichSuXem.ThoiGianXem
            };
        }
    }
}
