using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.FileStorage;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class TheTagRepository : ITheTagRepository
    {
        private readonly FilmDbContext _filmContext;

        public TheTagRepository(FilmDbContext filmContext)
        {
            _filmContext = filmContext;
        }

        public async Task<(List<TheTag> theTags, int total, int last)> GetAllTheTagsAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _filmContext.TheTag.AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var theTags = await query
                .OrderBy(t => t.TheTagId) // sắp xếp ổn định
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (theTags, total, last);
        }

        public async Task<TheTag?> GetByIdAsync(int theTagId, CancellationToken ct = default)
        {
            return await _filmContext.TheTag
                .FirstOrDefaultAsync(tt => tt.TheTagId == theTagId, ct);
        }

        public async Task<TheTag> CreateAsync(TheTag theTag, CancellationToken ct = default)
        {
            _filmContext.TheTag.Add(theTag);
            await _filmContext.SaveChangesAsync(ct);
            return theTag;
        }

        public async Task<TheTag> UpdateAsync(TheTag theTag, CancellationToken ct = default)
        {
            _filmContext.TheTag.Update(theTag);
            await _filmContext.SaveChangesAsync(ct);
            return theTag;
        }

        public async Task DeleteAsync(TheTag theTag, CancellationToken ct = default)
        {
            _filmContext.TheTag.Remove(theTag);
            await _filmContext.SaveChangesAsync(ct);
        }

        public async Task<List<TheTag>> SearchTheTagsAsync(string keyword, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<TheTag>();

            var normalizedKeyword = SlugHelper.NormalizeSearchText(keyword);

            var all = await _filmContext.TheTag
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var matched = all
                .Where(t => SlugHelper.NormalizeSearchText(t.TenT ?? "").Contains(normalizedKeyword)
                         || SlugHelper.NormalizeSearchText(t.MoTaT ?? "").Contains(normalizedKeyword))
                .OrderByDescending(t => t.TheTagId)
                .ToList();

            return matched;
        }

        public async Task<TheTag?> GetByMoTaTAsync(string moTaT, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(moTaT)) return null;

            var key = moTaT.Trim();
            return await _filmContext.TheTag
                .FirstOrDefaultAsync(tt => tt.MoTaT == key, ct);
        }

        public async Task<List<TheTag>> GetByMoTaTsAsync(List<string> moTaTs, CancellationToken ct = default)
        {
            if (moTaTs == null || moTaTs.Count == 0) return new List<TheTag>();

            var keys = moTaTs
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .Distinct()
                .ToList();

            var result = new List<TheTag>();
            foreach (var key in keys)
            {
                var item = await _filmContext.TheTag.FirstOrDefaultAsync(x => x.MoTaT == key, ct);
                if (item != null) result.Add(item);
            }
            return result;
        }
    }
}
