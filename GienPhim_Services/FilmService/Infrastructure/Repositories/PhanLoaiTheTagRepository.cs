using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class PhanLoaiTheTagRepository : IPhanLoaiTheTagRepository
    {
        private readonly FilmDbContext _context;

        public PhanLoaiTheTagRepository(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<(List<PhanLoaiTheTag> phanLoaiTheTags, int total, int last)> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _context.PhanLoaiTheTag
                .Include(x => x.Phim)
                .Include(x => x.TheTag)
                .AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var items = await query
                .OrderBy(x => x.PhimId)
                .ThenBy(x => x.TheTagId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (items, total, last);
        }

        public async Task<PhanLoaiTheTag?> GetByIdAsync(int phimId, int theTagId, CancellationToken cancellationToken = default)
        {
            return await _context.PhanLoaiTheTag
                .Include(x => x.Phim)
                .Include(x => x.TheTag)
                .FirstOrDefaultAsync(x => x.PhimId == phimId && x.TheTagId == theTagId, cancellationToken);
        }

        public async Task<PhanLoaiTheTag> CreateAsync(PhanLoaiTheTag phanLoaiTheTag, CancellationToken cancellationToken = default)
        {
            await _context.PhanLoaiTheTag.AddAsync(phanLoaiTheTag, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return phanLoaiTheTag;
        }

        public async Task<PhanLoaiTheTag> UpdateAsync(PhanLoaiTheTag phanLoaiTheTag, CancellationToken cancellationToken = default)
        {
            _context.PhanLoaiTheTag.Update(phanLoaiTheTag);
            await _context.SaveChangesAsync(cancellationToken);
            return phanLoaiTheTag;
        }

        public async Task DeleteAsync(PhanLoaiTheTag phanLoaiTheTag, CancellationToken cancellationToken = default)
        {
            _context.PhanLoaiTheTag.Remove(phanLoaiTheTag);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
