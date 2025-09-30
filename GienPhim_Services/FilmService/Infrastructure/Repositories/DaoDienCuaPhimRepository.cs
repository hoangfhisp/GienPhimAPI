using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class DaoDienCuaPhimRepository : IDaoDienCuaPhimRepository
    {
        private readonly FilmDbContext _context;

        public DaoDienCuaPhimRepository(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<(List<DaoDienCuaPhim> relations, int total, int last)> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _context.DaoDienCuaPhim
                .Include(r => r.Phim)
                .Include(r => r.DaoDien)
                .AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var relations = await query
                .OrderBy(r => r.PhimId)
                .ThenBy(r => r.DaoDienId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (relations, total, last);
        }

        public async Task<DaoDienCuaPhim?> GetByIdAsync(int phimId, int daoDienId, CancellationToken cancellationToken = default)
        {
            return await _context.DaoDienCuaPhim
                .Include(r => r.Phim)
                .Include(r => r.DaoDien)
                .FirstOrDefaultAsync(r => r.PhimId == phimId && r.DaoDienId == daoDienId, cancellationToken);
        }

        public async Task<DaoDienCuaPhim> CreateAsync(DaoDienCuaPhim entity, CancellationToken cancellationToken = default)
        {
            await _context.DaoDienCuaPhim.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<DaoDienCuaPhim> UpdateAsync(DaoDienCuaPhim entity, CancellationToken cancellationToken = default)
        {
            _context.DaoDienCuaPhim.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(DaoDienCuaPhim entity, CancellationToken cancellationToken = default)
        {
            _context.DaoDienCuaPhim.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
