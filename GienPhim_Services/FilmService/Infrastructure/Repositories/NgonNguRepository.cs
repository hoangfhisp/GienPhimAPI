using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.FileStorage;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class NgonNguRepository : INgonNguRepository
    {
        private readonly FilmDbContext _filmContext;

        public NgonNguRepository(FilmDbContext filmContext)
        {
            _filmContext = filmContext;
        }

        public async Task<(List<NgonNgu> ngonNgus, int total, int last)> GetAllNgonNgusAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _filmContext.NgonNgu.AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var ngonNgus = await query
                .OrderBy(nn => nn.NgonNguId) // ổn định kết quả
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (ngonNgus, total, last);
        }

        public async Task<NgonNgu?> GetByIdAsync(int ngonNguId, CancellationToken ct = default)
        {
            return await _filmContext.NgonNgu
                .FirstOrDefaultAsync(nn => nn.NgonNguId == ngonNguId, ct);
        }

        public async Task<NgonNgu> CreateAsync(NgonNgu ngonNgu, CancellationToken ct = default)
        {
            _filmContext.NgonNgu.Add(ngonNgu);
            await _filmContext.SaveChangesAsync(ct);
            return ngonNgu;
        }

        public async Task<NgonNgu> UpdateAsync(NgonNgu ngonNgu, CancellationToken ct = default)
        {
            _filmContext.NgonNgu.Update(ngonNgu);
            await _filmContext.SaveChangesAsync(ct);
            return ngonNgu;
        }

        public async Task DeleteAsync(NgonNgu ngonNgu, CancellationToken ct = default)
        {
            _filmContext.NgonNgu.Remove(ngonNgu);
            await _filmContext.SaveChangesAsync(ct);
        }

        public async Task<List<NgonNgu>> SearchNgonNgusAsync(string keyword, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<NgonNgu>();

            // Chuẩn hóa từ khóa (bỏ dấu + lower)
            var normalizedKeyword = SlugHelper.NormalizeSearchText(keyword);

            // Lấy tất cả ngôn ngữ
            var allLanguages = await _filmContext.NgonNgu
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            // So sánh bỏ dấu gần đúng
            var matched = allLanguages
                .Where(n => SlugHelper.NormalizeSearchText(n.TenNN ?? "").Contains(normalizedKeyword))
                .OrderByDescending(n => n.NgonNguId)
                .ToList();

            return matched;
        }
    }
}
