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
        public async Task<List<DaoDien>> GetAllDaoDiensAsync(CancellationToken cancellationToken = default)
        {
            return await _filmContext.DaoDien.ToListAsync(cancellationToken);
        }

        public async Task<DaoDien?> GetByIdAsync(int id, CancellationToken ct = default) // <— int
        {
            return await _filmContext.DaoDien
                .FirstOrDefaultAsync(x => x.DaoDienId == id, ct);
        }

        public async Task<DaoDien> CreateAsync(DaoDien e, CancellationToken ct = default)
        {
            _filmContext.DaoDien.Add(e);
            await _filmContext.SaveChangesAsync(ct);  // tự commit
            return e;
        }

        public async Task<DaoDien> UpdateAsync(DaoDien e, CancellationToken ct = default)
        {
            _filmContext.DaoDien.Update(e);
            await _filmContext.SaveChangesAsync(ct);  // tự commit
            return e;
        }

        public async Task DeleteAsync(DaoDien e, CancellationToken ct = default)
        {
            _filmContext.DaoDien.Remove(e);
            await _filmContext.SaveChangesAsync(ct);  // tự commit
        }

    }
}
