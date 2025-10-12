using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.FileStorage;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class TheLoaiRepository : ITheLoaiRepository
    {
        private readonly FilmDbContext _filmContext;

        public TheLoaiRepository(FilmDbContext filmContext)
        {
            _filmContext = filmContext;
        }

        public async Task<(List<TheLoai> theLoais, int total, int last)> GetAllTheLoaisAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _filmContext.TheLoai.AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var theLoais = await query
                .OrderBy(tl => tl.TheLoaiId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (theLoais, total, last);
        }

        public async Task<TheLoai?> GetByIdAsync(int theLoaiId, CancellationToken ct = default)
        {
            return await _filmContext.TheLoai
                .FirstOrDefaultAsync(tl => tl.TheLoaiId == theLoaiId, ct);
        }

        public async Task<TheLoai> CreateAsync(TheLoai theLoai, CancellationToken ct = default)
        {
            _filmContext.TheLoai.Add(theLoai);
            await _filmContext.SaveChangesAsync(ct);
            return theLoai;
        }

        public async Task<TheLoai> UpdateAsync(TheLoai theLoai, CancellationToken ct = default)
        {
            _filmContext.TheLoai.Update(theLoai);
            await _filmContext.SaveChangesAsync(ct);
            return theLoai;
        }

        public async Task DeleteAsync(TheLoai theLoai, CancellationToken ct = default)
        {
            _filmContext.TheLoai.Remove(theLoai);
            await _filmContext.SaveChangesAsync(ct);
        }

        public async Task<List<TheLoai>> SearchTheLoaisAsync(string keyword, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<TheLoai>();

            var normalizedKeyword = SlugHelper.NormalizeSearchText(keyword);

            var all = await _filmContext.TheLoai
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var matched = all
                .Where(t => SlugHelper.NormalizeSearchText(t.TenTL ?? "").Contains(normalizedKeyword)
                         || SlugHelper.NormalizeSearchText(t.MoTa ?? "").Contains(normalizedKeyword))
                .OrderByDescending(t => t.TheLoaiId)
                .ToList();

            return matched;
        }
    }
}
