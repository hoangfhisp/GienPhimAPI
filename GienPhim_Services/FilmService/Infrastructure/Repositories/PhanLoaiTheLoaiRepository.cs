using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class PhanLoaiTheLoaiRepository : IPhanLoaiTheLoaiRepository
    {
        private readonly FilmDbContext _context;

        public PhanLoaiTheLoaiRepository(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<(List<PhanLoaiTheLoai> phanLoaiTheLoais, int total, int last)> GetAllPhanLoaiTheLoaisAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _context.PhanLoaiTheLoai
                .Include(pl => pl.Phim)
                .Include(pl => pl.TheLoai)
                .AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var phanLoaiTheLoais = await query
                .OrderBy(pl => pl.PhimId)
                .ThenBy(pl => pl.TheLoaiId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (phanLoaiTheLoais, total, last);
        }

        public async Task<PhanLoaiTheLoai?> GetByIdAsync(int phimId, int theLoaiId, CancellationToken cancellationToken = default)
        {
            return await _context.PhanLoaiTheLoai
                .Include(pl => pl.Phim)
                .Include(pl => pl.TheLoai)
                .FirstOrDefaultAsync(pl => pl.PhimId == phimId && pl.TheLoaiId == theLoaiId, cancellationToken);
        }

        public async Task<PhanLoaiTheLoai> CreateAsync(PhanLoaiTheLoai phanLoaiTheLoai, CancellationToken cancellationToken = default)
        {
            await _context.PhanLoaiTheLoai.AddAsync(phanLoaiTheLoai, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return phanLoaiTheLoai;
        }

        public async Task<PhanLoaiTheLoai> UpdateAsync(PhanLoaiTheLoai phanLoaiTheLoai, CancellationToken cancellationToken = default)
        {
            _context.PhanLoaiTheLoai.Update(phanLoaiTheLoai);
            await _context.SaveChangesAsync(cancellationToken);
            return phanLoaiTheLoai;
        }

        public async Task DeleteAsync(PhanLoaiTheLoai phanLoaiTheLoai, CancellationToken cancellationToken = default)
        {
            _context.PhanLoaiTheLoai.Remove(phanLoaiTheLoai);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
