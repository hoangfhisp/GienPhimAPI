using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.FileStorage;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class DienVienRepository : IDienVienRepository
    {
        private readonly FilmDbContext _filmContext;

        public DienVienRepository(FilmDbContext filmContext)
        {
            _filmContext = filmContext;
        }

        public async Task<(List<DienVien> dienViens, int total, int last)> GetAllDienViensAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _filmContext.DienVien.AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var dienViens = await query
                .OrderBy(dv => dv.DienVienId) // nên có order để phân trang ổn định
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (dienViens, total, last);
        }

        public async Task<DienVien?> GetByIdAsync(int dienVienId, CancellationToken ct = default)
        {
            return await _filmContext.DienVien
                .FirstOrDefaultAsync(dv => dv.DienVienId == dienVienId, ct);
        }

        public async Task<DienVien> CreateAsync(DienVien dienVien, CancellationToken ct = default)
        {
            _filmContext.DienVien.Add(dienVien);
            await _filmContext.SaveChangesAsync(ct);
            return dienVien;
        }

        public async Task<DienVien> UpdateAsync(DienVien dienVien, CancellationToken ct = default)
        {
            _filmContext.DienVien.Update(dienVien);
            await _filmContext.SaveChangesAsync(ct);
            return dienVien;
        }

        public async Task DeleteAsync(DienVien dienVien, CancellationToken ct = default)
        {
            _filmContext.DienVien.Remove(dienVien);
            await _filmContext.SaveChangesAsync(ct);
        }

        public async Task<List<DienVien>> SearchDienViensAsync(string keyword, CancellationToken cancellationToken = default)
        {
            // 🛡️ Kiểm tra input
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<DienVien>();

            // Chuẩn hóa từ khóa (bỏ dấu + lower)
            var normalizedKeyword = SlugHelper.NormalizeSearchText(keyword);

            // Lấy tất cả diễn viên
            var allActors = await _filmContext.DienVien
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            // Lọc gần đúng (so sánh bỏ dấu)
            var matched = allActors
                .Where(dv => SlugHelper.NormalizeSearchText(dv.TenDV ?? "").Contains(normalizedKeyword)
                          || SlugHelper.NormalizeSearchText(dv.MoTaDV ?? "").Contains(normalizedKeyword))
                .OrderByDescending(dv => dv.DienVienId)
                .ToList();

            return matched;
        }
    }
}
