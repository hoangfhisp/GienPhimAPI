using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalid;
using GienPhim_Services.FilmService.Domain.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class PhimDomainService : IPhimDomainService
    {
        public void EnsureCanCreate(Phim phim, string avatar, string poster)
        {
            // 1. Kiểm tra null
            if (phim == null)
                throw new InvalidPhimException("Phim không được null");

            // 2. Validate tên phim
            if (string.IsNullOrWhiteSpace(phim.TenP))
                throw new InvalidPhimException("Tên phim không được để trống");

            if (phim.TenP.Length > 200)
                throw new InvalidPhimException("Tên phim vượt quá 200 ký tự");

            // 3. Validate quốc gia & ngôn ngữ
            if (phim.QuocGiaId <= 0)
                throw new InvalidPhimException("Phim phải có quốc gia hợp lệ");

            if (phim.NgonNguId <= 0)
                throw new InvalidPhimException("Phim phải có ngôn ngữ hợp lệ");

            // 4. Validate ngày phát hành
            if (phim.NgayPhatHanhP == default)
                throw new InvalidPhimException("Ngày phát hành phim không hợp lệ");

            ValidateAvatar(avatar);
            ValidatePoster(poster);

            phim.AvatarP = avatar;
            phim.AvatarPoster = poster;
            phim.ThoiGianTao = DateTime.UtcNow;
            phim.ThoiGianCapNhat = DateTime.UtcNow;
        }

        public void EnsureCanUpdate(Phim phim, string? avatar, string? poster)
        {
            if (phim == null)
                throw new InvalidPhimException("Phim không được null");

            if (string.IsNullOrWhiteSpace(phim.TenP))
                throw new InvalidPhimException("Tên phim không được để trống");

            if (phim.QuocGiaId <= 0)
                throw new InvalidPhimException("Phim phải có quốc gia hợp lệ");

            if (phim.NgonNguId <= 0)
                throw new InvalidPhimException("Phim phải có ngôn ngữ hợp lệ");

            if (phim.NgayPhatHanhP == default)
                throw new InvalidPhimException("Ngày phát hành phim không hợp lệ");

            if (!string.IsNullOrWhiteSpace(avatar))
            {
                ValidateAvatar(avatar);
                phim.AvatarP = avatar;
            }

            if (!string.IsNullOrWhiteSpace(poster))
            {
                ValidatePoster(poster);
                phim.AvatarPoster = poster;
            }

            phim.ThoiGianCapNhat = DateTime.UtcNow;
        }

        public void EnsureCanDelete(Phim phim)
        {
            if (phim == null)
                throw new InvalidPhimException("Phim không tồn tại");

        }

        public void EnsureCanCreateApiFilm(Phim phim)
        {
            if (phim == null) throw new InvalidPhimException("Dữ liệu phim API không hợp lệ.");
            if (string.IsNullOrWhiteSpace(phim.TenP))
                throw new InvalidPhimException("Phim từ API phải có tên.");

            if (phim.TenP.Length > 200)
                phim.TenP = phim.TenP.Substring(0, 200);

            if (phim.NgayPhatHanhP == default) phim.NgayPhatHanhP = DateTime.UtcNow;

            // Nếu có ảnh thì validate, nếu không thì fallback
            if (!string.IsNullOrWhiteSpace(phim.AvatarP)) ValidateAvatar(phim.AvatarP);
            else
                phim.AvatarP = "/images/films/default-avatar.jpg";

            if (!string.IsNullOrWhiteSpace(phim.AvatarPoster)) ValidatePoster(phim.AvatarPoster);
            else
                phim.AvatarPoster = "/images/banners/default-poster.jpg";


            phim.ThoiGianTao = DateTime.UtcNow;
            phim.ThoiGianCapNhat = DateTime.UtcNow;
        }

        // Kiểm tra avatar & poster
        public void ValidateAvatar(string avatar)
        {
            if (string.IsNullOrWhiteSpace(avatar))
                throw new InvalidPhimException("Avatar phim không hợp lệ");
        }

        public void ValidatePoster(string poster)
        {
            if (string.IsNullOrWhiteSpace(poster))
                throw new InvalidPhimException("Poster phim không hợp lệ");
        }
    }
}
