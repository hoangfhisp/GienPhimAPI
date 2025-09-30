using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class DienVienCuaPhimRepository : IDienVienCuaPhimRepository
    {
        private readonly FilmDbContext _context;

        public DienVienCuaPhimRepository(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<(List<DienVienCuaPhim> relations, int total, int last)> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _context.DienVienCuaPhim
                .Include(r => r.Phim)
                .Include(r => r.DienVien)
                .AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var relations = await query
                .OrderBy(r => r.PhimId)
                .ThenBy(r => r.DienVienId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (relations, total, last);
        }

        public async Task<DienVienCuaPhim?> GetByIdAsync(int phimId, int dienVienId, CancellationToken cancellationToken = default)
        {
            return await _context.DienVienCuaPhim
                .Include(r => r.Phim)
                .Include(r => r.DienVien)
                .FirstOrDefaultAsync(r => r.PhimId == phimId && r.DienVienId == dienVienId, cancellationToken);
        }

        public async Task<DienVienCuaPhim> CreateAsync(DienVienCuaPhim entity, CancellationToken cancellationToken = default)
        {
            await _context.DienVienCuaPhim.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<DienVienCuaPhim> UpdateAsync(DienVienCuaPhim entity, CancellationToken cancellationToken = default)
        {
            _context.DienVienCuaPhim.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(DienVienCuaPhim entity, CancellationToken cancellationToken = default)
        {
            _context.DienVienCuaPhim.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
