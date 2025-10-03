using GienPhim_Services.FilmService.Application.DTOs.Client.TapPhim;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Entities;
using System.Text;
using System.Text.RegularExpressions;

namespace GienPhim_Services.FilmService.Application.Factories
{
    public class TapPhimFactory : ITapPhimFactory
    {
        public TapPhim Create(TapPhimCreateDTO dto, string? linkChinhFileName)
        {
            return new TapPhim
            {
                TapId = Guid.NewGuid().ToString("N"),
                PhimId = dto.PhimId,
                TenTap = dto.TenTap.Trim(),
                SlugTap = string.IsNullOrWhiteSpace(dto.SlugTap) ? GenerateSlug(dto.TenTap) : dto.SlugTap.Trim().ToLower(),
                TenFile = dto.TenFile.Trim(),
                LinkChinh = linkChinhFileName, // lưu tên file video sau khi save
                LinkPhu = dto.LinkPhu.Trim(),
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public TapPhim Update(TapPhimUpdateDTO dto, string? linkChinhFileName)
        {
            return new TapPhim
            {
                TapId = dto.TapId,
                PhimId = dto.PhimId,
                TenTap = dto.TenTap.Trim(),
                SlugTap = string.IsNullOrWhiteSpace(dto.SlugTap) ? GenerateSlug(dto.TenTap) : dto.SlugTap.Trim().ToLower(),
                TenFile = dto.TenFile.Trim(),
                LinkChinh = linkChinhFileName,
                LinkPhu = dto.LinkPhu.Trim(),
                ThoiGianCapNhat = DateTime.UtcNow
            };
        }

        public TapPhim Delete(TapPhimDeleteDTO dto)
        {
            return new TapPhim
            {
                TapId = dto.TapId
            };
        }

        // Private helper để tạo slug từ TenTap
        private string GenerateSlug(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            string normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in normalized)
            {
                var uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            string noDiacritics = sb.ToString().Normalize(NormalizationForm.FormC);
            string slug = Regex.Replace(noDiacritics.ToLower(), @"\s+", "-");
            slug = Regex.Replace(slug, @"[^a-z0-9-]", "");
            return slug;
        }
    }
}
