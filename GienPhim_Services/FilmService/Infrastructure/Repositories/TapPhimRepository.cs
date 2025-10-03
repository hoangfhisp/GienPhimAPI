using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class TapPhimRepository : ITapPhimRepository
    {
        private readonly FilmDbContext _context;

        public TapPhimRepository(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<(List<TapPhim> tapPhims, int total, int last)> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _context.TapPhim
                .Include(tp => tp.Phim) // load phim để lấy TenP
                .AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var tapPhims = await query
                .OrderBy(tp => tp.TapId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (tapPhims, total, last);
        }

        public async Task<TapPhim?> GetByIdAsync(string tapId, CancellationToken cancellationToken = default)
        {
            return await _context.TapPhim
                .Include(tp => tp.Phim)
                .FirstOrDefaultAsync(tp => tp.TapId == tapId, cancellationToken);
        }

        public async Task<List<TapPhim>> GetByPhimIdAsync(int phimId, CancellationToken cancellationToken = default)
        {
            return await _context.TapPhim
                .Where(t => t.PhimId == phimId)
                .OrderBy(t => t.TenTap)
                .ToListAsync(cancellationToken);
        }

        public async Task<TapPhim> CreateAsync(TapPhim tapPhim, CancellationToken cancellationToken = default)
        {
            await _context.TapPhim.AddAsync(tapPhim, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return tapPhim;
        }

        public async Task<TapPhim> UpdateAsync(TapPhim tapPhim, CancellationToken cancellationToken = default)
        {
            _context.TapPhim.Update(tapPhim);
            await _context.SaveChangesAsync(cancellationToken);
            return tapPhim;
        }

        public async Task DeleteAsync(TapPhim tapPhim, CancellationToken cancellationToken = default)
        {
            _context.TapPhim.Remove(tapPhim);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
