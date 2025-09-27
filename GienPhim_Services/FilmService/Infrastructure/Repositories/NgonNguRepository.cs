using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class NgonNguRepository : INgonNguRepository
    {
        private readonly FilmDbContext _filmContext;

        public NgonNguRepository(FilmDbContext filmContext)
        {
            _filmContext = filmContext;
        }

        public async Task<List<NgonNgu>> GetAllNgonNgusAsync(CancellationToken cancellationToken = default)
        {
            return await _filmContext.NgonNgu.ToListAsync(cancellationToken);
        }

        public async Task<NgonNgu?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _filmContext.NgonNgu
                .FirstOrDefaultAsync(x => x.NgonNguId == id, ct);
        }

        public async Task<NgonNgu> CreateAsync(NgonNgu e, CancellationToken ct = default)
        {
            _filmContext.NgonNgu.Add(e);
            await _filmContext.SaveChangesAsync(ct);
            return e;
        }

        public async Task<NgonNgu> UpdateAsync(NgonNgu e, CancellationToken ct = default)
        {
            _filmContext.NgonNgu.Update(e);
            await _filmContext.SaveChangesAsync(ct);
            return e;
        }

        public async Task DeleteAsync(NgonNgu e, CancellationToken ct = default)
        {
            _filmContext.NgonNgu.Remove(e);
            await _filmContext.SaveChangesAsync(ct);
        }
    }
}
