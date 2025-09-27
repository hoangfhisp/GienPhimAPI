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

        public async Task<List<TheTag>> GetAllTheTagsAsync(CancellationToken cancellationToken = default)
        {
            return await _filmContext.TheTag.ToListAsync(cancellationToken);
        }

        public async Task<TheTag?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _filmContext.TheTag
                .FirstOrDefaultAsync(x => x.TheTagId == id, ct);
        }

        public async Task<TheTag> CreateAsync(TheTag e, CancellationToken ct = default)
        {
            _filmContext.TheTag.Add(e);
            await _filmContext.SaveChangesAsync(ct);
            return e;
        }

        public async Task<TheTag> UpdateAsync(TheTag e, CancellationToken ct = default)
        {
            _filmContext.TheTag.Update(e);
            await _filmContext.SaveChangesAsync(ct);
            return e;
        }

        public async Task DeleteAsync(TheTag e, CancellationToken ct = default)
        {
            _filmContext.TheTag.Remove(e);
            await _filmContext.SaveChangesAsync(ct);
        }
    }
}
