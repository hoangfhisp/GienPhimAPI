using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class PhimResponseMapper : IPhimResponseMapper
    {
        public PhimResponseDTO ToDto(Phim phim)
        {
            return new PhimResponseDTO
            {
                PhimId = phim.PhimId,
                TenP = phim.TenP,
                MoTaP = phim.MoTaP,
                TenCTSXP = phim.TenCTSXP,
                NgayPhatHanhP = phim.NgayPhatHanhP,
                LuotThichP = phim.LuotThichP,
                LuotDislikeP = phim.LuotDislikeP,
                LuotXemP = phim.LuotXemP,
                ThuoiLuongP = phim.ThuoiLuongP,
                AvatarP = phim.AvatarP,
                AvatarPoster = phim.AvatarPoster,
                NguonP = phim.NguonP,
                SoTapP = phim.SoTapP,
                TraiLerP = phim.TraiLerP,
                QuocGia = phim.QuocGia != null ? phim.QuocGia.TenQG : string.Empty,
                NgonNgu = phim.NgonNgu != null ? phim.NgonNgu.TenNN : string.Empty,

                // 🔥 Thêm 4 dòng này:
                TheLoais = phim.PhanLoaiTheLoais?
                    .Where(pl => pl.TheLoai != null)
                    .Select(pl => pl.TheLoai.TenTL)
                    .Distinct()
                    .ToList() ?? new List<string>(),

                DaoDiens = phim.DaoDienCuaPhims?
                    .Where(dp => dp.DaoDien != null)
                    .Select(dp => dp.DaoDien.TenDD)
                    .Distinct()
                    .ToList() ?? new List<string>(),

                DienViens = phim.DienVienCuaPhims?
                    .Where(dv => dv.DienVien != null)
                    .Select(dv => dv.DienVien.TenDV)
                    .Distinct()
                    .ToList() ?? new List<string>(),

                TheTags = phim.PhanLoaiTheTags?
                    .Where(t => t.TheTag != null)
                    .Select(t => t.TheTag.TenT)
                    .Distinct()
                    .ToList() ?? new List<string>(),

                ThoiGianTao = phim.ThoiGianTao,
                ThoiGianCapNhat = phim.ThoiGianCapNhat
            };
        }

        public List<PhimResponseDTO> ToDtoList(List<Phim> phims)
        {
            return phims.Select(p => ToDto(p)).ToList();
        }

        public UpdatePhimResponseDTO ToUpdateDto(Phim phim)
        {
            return new UpdatePhimResponseDTO
            {
                PhimId = phim.PhimId,
                TenP = phim.TenP,
                MoTaP = phim.MoTaP,
                TenCTSXP = phim.TenCTSXP,
                NgayPhatHanhP = phim.NgayPhatHanhP,
                ThuoiLuongP = phim.ThuoiLuongP,
                AvatarP = phim.AvatarP,
                AvatarPoster = phim.AvatarPoster,
                NguonP = phim.NguonP,
                SoTapP = phim.SoTapP,
                TraiLerP = phim.TraiLerP,
                QuocGia = phim.QuocGia != null ? phim.QuocGia.TenQG : string.Empty,
                NgonNgu = phim.NgonNgu != null ? phim.NgonNgu.TenNN : string.Empty,
                ThoiGianTao = phim.ThoiGianTao,
                ThoiGianCapNhat = phim.ThoiGianCapNhat
            };
        }

        public List<UpdatePhimResponseDTO> ToUpdateDtoList(List<Phim> phims)
        {
            return phims.Select(p => ToUpdateDto(p)).ToList();
        }
    }
}
