using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
    }
}
