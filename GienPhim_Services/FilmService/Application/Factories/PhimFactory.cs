using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class PhimFactory : IPhimFactory
    {
        public Phim CreatePhim(PhimCreateDTO dto, string avatar, string poster)
        {
            return new Phim
            {
                TenP = dto.TenP?.Trim() ?? string.Empty,
                MoTaP = string.IsNullOrWhiteSpace(dto.MoTaP) ? null : dto.MoTaP.Trim(),
                QuocGiaId = dto.QuocGiaId,
                NgonNguId = dto.NgonNguId,
                TenCTSXP = dto.TenCTSXP?.Trim() ?? string.Empty,
                NgayPhatHanhP = dto.NgayPhatHanhP,
                ThuoiLuongP = dto.ThuoiLuongP,
                NguonP = dto.NguonP,
                SoTapP = dto.SoTapP,
                TraiLerP = dto.TraiLerP,
                AvatarP = avatar,
                AvatarPoster = poster,
                LuotThichP = 0,
                LuotDislikeP = 0,
                LuotXemP = 0,
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public Phim UpdatePhim(PhimUpdateDTO dto, string avatar, string poster)
        {
            return new Phim
            {
                PhimId = dto.PhimId,
                TenP = dto.TenP?.Trim() ?? string.Empty,
                MoTaP = string.IsNullOrWhiteSpace(dto.MoTaP) ? null : dto.MoTaP.Trim(),
                QuocGiaId = dto.QuocGiaId,
                NgonNguId = dto.NgonNguId,
                TenCTSXP = dto.TenCTSXP?.Trim() ?? string.Empty,
                NgayPhatHanhP = dto.NgayPhatHanhP,
                ThuoiLuongP = dto.ThuoiLuongP,
                NguonP = dto.NguonP,
                SoTapP = dto.SoTapP,
                TraiLerP = dto.TraiLerP,
                AvatarP = avatar,
                AvatarPoster = poster,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public Phim DeletePhim(PhimDeleteDTO dto)
        {
            return new Phim
            {
                PhimId = dto.PhimId
            };
        }
    }
}
