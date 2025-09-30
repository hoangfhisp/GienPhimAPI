using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class PhimRepository : IPhimRepository
    {
        private readonly FilmDbContext _context;

        public PhimRepository(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Phim> phims, int total, int last)> GetAllPhimsAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _context.Phim
                .Include(p => p.QuocGia)
                .Include(p => p.NgonNgu)
                .AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var phims = await query
                .OrderBy(p => p.PhimId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (phims, total, last);
        }

        public async Task<Phim?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Phim
                .Include(p => p.QuocGia)
                .Include(p => p.NgonNgu)
                .FirstOrDefaultAsync(p => p.PhimId == id, cancellationToken);
        }

        public async Task<Phim> CreateAsync(Phim phim, CancellationToken cancellationToken = default)
        {
            await _context.Phim.AddAsync(phim, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return phim;
        }

        public async Task<Phim> UpdateAsync(Phim phim, CancellationToken cancellationToken = default)
        {
            _context.Phim.Update(phim);
            await _context.SaveChangesAsync(cancellationToken);
            return phim;
        }

        public async Task DeleteAsync(Phim phim, CancellationToken cancellationToken = default)
        {
            _context.Phim.Remove(phim);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
