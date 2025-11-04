using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Domain.Entities;

namespace GienPhim_Services.FilmService.Application.Mappers
{
    public class ApiPhimMapper : IApiPhimMapper
    {
        /* ------------------- Ánh xạ PHIM ------------------- */
        public Phim ToPhimEntity(ApiPhimDTO dto, NgonNgu ngonNgu, QuocGia quocGia, string slug)
        {
            var movie = dto.Movie!;
            return new Phim
            {
                TenP = movie.Name ?? "Không rõ",
                MoTaP = movie.Content ?? "Đang cập nhật",
                TenCTSXP = movie.Origin_Name ?? "Chưa có",
                ThuoiLuongP = movie.Time ?? "Không rõ",
                AvatarP = movie.Thumb_Url ?? "/images/films/default-avatar.jpg",
                AvatarPoster = movie.Poster_Url ?? "/images/banners/default-poster.jpg",
                NguonP = dto.Movie?.Slug ?? slug,
                LuotXemP = movie.View,
                NgayPhatHanhP = movie.Year > 0 ? new DateTime(movie.Year, 1, 1) : DateTime.UtcNow,
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow,
                NgonNgu = ngonNgu,
                QuocGia = quocGia,
                TraiLerP = movie.Trailer_Url,
            };
        }

        /* ------------------- Ánh xạ NGÔN NGỮ ------------------- */
        public NgonNgu ToNgonNguEntity(ApiPhimDTO dto)
        {
            var lang = dto.Movie?.Lang ?? "Không rõ";
            return new NgonNgu
            {
                TenNN = lang,
                MoTaNN = $"Ngôn ngữ phim: {lang}",
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        /* ------------------- Ánh xạ QUỐC GIA ------------------- */
        public QuocGia ToQuocGiaEntity(ApiPhimDTO dto)
        {
            var country = dto.Movie?.Country?.FirstOrDefault();
            return new QuocGia
            {
                TenQG = country?.Name ?? "Không rõ",
                MoTaQG = country?.Slug ?? "khong-ro",
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        /* ------------------- Ánh xạ THỂ LOẠI ------------------- */
        public List<TheLoai> ToTheLoaiEntities(ApiPhimDTO dto)
        {
            var list = new List<TheLoai>();
            if (dto.Movie?.Category == null) return list;

            foreach (var cat in dto.Movie.Category)
            {
                list.Add(new TheLoai
                {
                    TenTL = cat.Name ?? "Không rõ",
                    MoTa = cat.Slug ?? "khong-ro",
                    ThoiGianTao = DateTime.UtcNow,
                    ThoiGianCapNhat = DateTime.UtcNow
                });
            }
            return list;
        }

        public List<PhanLoaiTheLoai> ToPhanLoaiTheLoaiEntities(int phimId, IEnumerable<TheLoai> theLoais)
        {
            return theLoais.Select(t => new PhanLoaiTheLoai
            {
                PhimId = phimId,
                TheLoai = t,
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            }).ToList();
        }

        /* ------------------- Ánh xạ DIỄN VIÊN ------------------- */
        public List<DienVien> ToDienVienEntities(ApiPhimDTO dto)
        {
            var list = new List<DienVien>();
            if (dto.Movie?.Actor == null) return list;

            foreach (var name in dto.Movie.Actor)
            {
                list.Add(new DienVien
                {
                    TenDV = name,
                    MoTaDV = name,
                    ThoiGianTao = DateTime.UtcNow,
                    ThoiGianCapNhat = DateTime.UtcNow
                });
            }
            return list;
        }

        public List<DienVienCuaPhim> ToDienVienCuaPhimEntities(int phimId, IEnumerable<DienVien> dienViens)
        {
            return dienViens.Select(d => new DienVienCuaPhim
            {
                PhimId = phimId,
                DienVienId = d.DienVienId,
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            }).ToList();
        }

        /* ------------------- Ánh xạ ĐẠO DIỄN ------------------- */
        public List<DaoDien> ToDaoDienEntities(ApiPhimDTO dto)
        {
            var list = new List<DaoDien>();
            if (dto.Movie?.Director == null) return list;

            foreach (var name in dto.Movie.Director)
            {
                list.Add(new DaoDien
                {
                    TenDD = name,
                    MoTaDD = name,
                    ThoiGianTao = DateTime.UtcNow,
                    ThoiGianCapNhat = DateTime.UtcNow
                });
            }
            return list;
        }

        public List<DaoDienCuaPhim> ToDaoDienCuaPhimEntities(int phimId, IEnumerable<DaoDien> daoDiens)
        {
            return daoDiens.Select(d => new DaoDienCuaPhim
            {
                PhimId = phimId,
                DaoDienId = d.DaoDienId,
                VaiTroDG = "Đang cập nhật",
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            }).ToList();
        }

        /* ------------------- Ánh xạ TẬP PHIM ------------------- */
        public List<TapPhim> ToTapPhimEntities(ApiPhimDTO dto, int phimId)
        {
            var list = new List<TapPhim>();
            if (dto.Episodes == null) return list;

            foreach (var server in dto.Episodes)
            {
                if (server.Server_Data == null) continue;

                foreach (var tap in server.Server_Data)
                {
                    list.Add(new TapPhim
                    {
                        TapId = Guid.NewGuid().ToString(),
                        PhimId = phimId,
                        TenTap = tap.Name ?? "Tập ?",
                        SlugTap = tap.Slug ?? "khong-ro",
                        TenFile = tap.Filename ?? "khong-ro",
                        LinkPhu = tap.Link_Embed ?? "",
                        LinkChinh = tap.Link_M3u8 ?? "",
                        ThoiGianTao = DateTime.UtcNow,
                        ThoiGianCapNhat = DateTime.UtcNow
                    });
                }
            }
            return list;
        }

        /* ------------------- UPDATE PHIM & TẬP PHIM ------------------- */

        // Cập nhật thông tin cơ bản của phim từ API (poster, trailer, mô tả, lượt xem,…)
        public void UpdatePhimEntity(Phim existing, ApiPhimDTO dto)
        {
            if (dto.Movie == null) return;
            var movie = dto.Movie;

            existing.MoTaP = movie.Content ?? existing.MoTaP;
            existing.AvatarP = movie.Thumb_Url ?? existing.AvatarP;
            existing.AvatarPoster = movie.Poster_Url ?? existing.AvatarPoster;
            existing.TraiLerP = movie.Trailer_Url ?? existing.TraiLerP;
            existing.LuotXemP = movie.View > 0 ? movie.View : existing.LuotXemP;
            existing.ThuoiLuongP = movie.Time ?? existing.ThuoiLuongP;
            existing.ThoiGianCapNhat = DateTime.UtcNow;
        }

        // Cập nhật danh sách tập phim (thêm tập mới hoặc update link)
        public void UpdateTapPhimEntities(Phim existing, ApiPhimDTO dto)
        {
            if (dto.Episodes == null) return;

            foreach (var server in dto.Episodes)
            {
                if (server.Server_Data == null) continue;

                foreach (var tap in server.Server_Data)
                {
                    // Tìm tập cũ theo slug
                    var existingTap = existing.TapPhims
                        .FirstOrDefault(t => t.SlugTap == tap.Slug);

                    // Nếu chưa có → thêm mới
                    if (existingTap == null)
                    {
                        existing.TapPhims.Add(new TapPhim
                        {
                            TapId = Guid.NewGuid().ToString(),
                            PhimId = existing.PhimId,
                            TenTap = tap.Name ?? "Tập ?",
                            SlugTap = tap.Slug ?? "khong-ro",
                            TenFile = tap.Filename ?? "khong-ro",
                            LinkPhu = tap.Link_Embed ?? "",
                            LinkChinh = tap.Link_M3u8 ?? "",
                            ThoiGianTao = DateTime.UtcNow,
                            ThoiGianCapNhat = DateTime.UtcNow
                        });
                    }
                    else
                    {
                        // Nếu đã có nhưng link đổi → cập nhật
                        bool changed = false;

                        if (existingTap.LinkChinh != tap.Link_M3u8 && !string.IsNullOrWhiteSpace(tap.Link_M3u8))
                        {
                            existingTap.LinkChinh = tap.Link_M3u8;
                            changed = true;
                        }
                        if (existingTap.LinkPhu != tap.Link_Embed && !string.IsNullOrWhiteSpace(tap.Link_Embed))
                        {
                            existingTap.LinkPhu = tap.Link_Embed;
                            changed = true;
                        }

                        if (changed)
                        {
                            existingTap.ThoiGianCapNhat = DateTime.UtcNow;
                        }
                    }
                }
            }
        }
    }
}