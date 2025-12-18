using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.FileStorage;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class QuocGiaRepository : IQuocGiaRepository
    {
        private readonly FilmDbContext _filmContext;

        public QuocGiaRepository(FilmDbContext filmContext)
        {
            _filmContext = filmContext;
        }

        public async Task<(List<QuocGia> quocGias, int total, int last)> GetAllQuocGiasAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _filmContext.QuocGia.AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var quocGias = await query
                .OrderBy(qg => qg.QuocGiaId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (quocGias, total, last);
        }

        public async Task<QuocGia?> GetByIdAsync(int quocGiaId, CancellationToken ct = default)
        {
            return await _filmContext.QuocGia
                .FirstOrDefaultAsync(qg => qg.QuocGiaId == quocGiaId, ct);
        }

        public async Task<QuocGia> CreateAsync(QuocGia quocGia, CancellationToken ct = default)
        {
            _filmContext.QuocGia.Add(quocGia);
            await _filmContext.SaveChangesAsync(ct);
            return quocGia;
        }

        public async Task<QuocGia> UpdateAsync(QuocGia quocGia, CancellationToken ct = default)
        {
            _filmContext.QuocGia.Update(quocGia);
            await _filmContext.SaveChangesAsync(ct);
            return quocGia;
        }

        public async Task DeleteAsync(QuocGia quocGia, CancellationToken ct = default)
        {
            _filmContext.QuocGia.Remove(quocGia);
            await _filmContext.SaveChangesAsync(ct);
        }

        public async Task<List<QuocGia>> SearchQuocGiasAsync(string keyword, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<QuocGia>();

            // Chuẩn hóa từ khóa (bỏ dấu + lower)
            var normalizedKeyword = SlugHelper.NormalizeSearchText(keyword);

            // Lấy tất cả quốc gia
            var allCountries = await _filmContext.QuocGia
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            // So sánh bỏ dấu gần đúng
            var matched = allCountries
                .Where(q => SlugHelper.NormalizeSearchText(q.TenQG ?? "").Contains(normalizedKeyword)
                         || SlugHelper.NormalizeSearchText(q.MoTaQG ?? "").Contains(normalizedKeyword))
                .OrderByDescending(q => q.QuocGiaId)
                .ToList();

            return matched;
        }

        public async Task<QuocGia?> GetByMoTaQGAsync(string moTaQG, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(moTaQG)) return null;

            var key = moTaQG.Trim();
            return await _filmContext.QuocGia
                .FirstOrDefaultAsync(qg => qg.MoTaQG == key, ct);
        }

        public async Task<List<QuocGia>> GetByMoTaQGsAsync(List<string> moTaQGs, CancellationToken ct = default)
        {
            if (moTaQGs == null || moTaQGs.Count == 0) return new List<QuocGia>();

            var keys = moTaQGs
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .Distinct()
                .ToList();

            var result = new List<QuocGia>();
            foreach (var key in keys)
            {
                var item = await _filmContext.QuocGia.FirstOrDefaultAsync(x => x.MoTaQG == key, ct);
                if (item != null) result.Add(item);
            }
            return result;
        }
    }
}
