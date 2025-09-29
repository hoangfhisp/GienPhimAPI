using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class DaoDienRepository : IDaoDienRepository
    {
        private readonly FilmDbContext _filmContext;

        public DaoDienRepository(FilmDbContext filmContext)
        {
            _filmContext = filmContext;
        }

        // Lấy all đạo diễn 
        public async Task<(List<DaoDien> daoDiens, int total, int last)> GetAllDaoDiensAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _filmContext.DaoDien.AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var daoDiens = await query
                .OrderBy(dd => dd.DaoDienId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (daoDiens, total, last);
        }

        public async Task<DaoDien?> GetByIdAsync(int daoDienId, CancellationToken ct = default)
        {
            return await _filmContext.DaoDien
                .FirstOrDefaultAsync(daoDien => daoDien.DaoDienId == daoDienId, ct);
        }

        public async Task<DaoDien> CreateAsync(DaoDien daoDien, CancellationToken ct = default)
        {
            _filmContext.DaoDien.Add(daoDien);
            await _filmContext.SaveChangesAsync(ct);  // tự commit
            return daoDien;
        }

        public async Task<DaoDien> UpdateAsync(DaoDien daoDien, CancellationToken ct = default)
        {
            _filmContext.DaoDien.Update(daoDien);
            await _filmContext.SaveChangesAsync(ct);  // tự commit
            return daoDien;
        }

        public async Task DeleteAsync(DaoDien daoDien, CancellationToken ct = default)
        {
            _filmContext.DaoDien.Remove(daoDien);
            await _filmContext.SaveChangesAsync(ct);  // tự commit
        }
    }
}
