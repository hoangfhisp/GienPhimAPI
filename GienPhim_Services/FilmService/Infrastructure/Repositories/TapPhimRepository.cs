using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.FileStorage;

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
                .AsNoTracking()
                .Include(tp => tp.Phim)
                .Where(tp => tp.PhimId == phimId)
                .OrderBy(tp => tp.TenTap)
                .ToListAsync(cancellationToken);
        }

        public async Task<TapPhim> FindByTapSugAndPhimId(string tapSlug,int phimId, CancellationToken cancellationToken = default)
        {
            return await _context.TapPhim.FirstOrDefaultAsync(t => t.SlugTap == tapSlug && t.PhimId == phimId,cancellationToken);
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

        public async Task<(List<TapPhim> tapPhims, int total, int last)>SearchTapPhimAsync(string keyword, int page, int limit, CancellationToken ct = default)
        {
            page = page <= 0 ? 1 : page;
            limit = limit <= 0 ? 10 : limit;

            var key = SlugHelper.NormalizeSearchText(keyword);

            var all = await _context.TapPhim
                .AsNoTracking()
                .Include(tp => tp.Phim)
                .Select(tp => new TapPhim
                {
                    TapId = tp.TapId,
                    TenTap = tp.TenTap,
                    SlugTap = tp.SlugTap,
                    TenFile = tp.TenFile,
                    PhimId = tp.PhimId,

                    // BỔ SUNG 2 field này
                    LinkChinh = tp.LinkChinh,
                    LinkPhu = tp.LinkPhu,

                    ThoiGianCapNhat = tp.ThoiGianCapNhat,

                    Phim = tp.Phim == null ? null : new Phim
                    {
                        PhimId = tp.Phim.PhimId,
                        TenP = tp.Phim.TenP
                    }
                })
                .ToListAsync(ct);

            var matched = all
                .Where(tp =>
                    SlugHelper.NormalizeSearchText(tp.TenTap ?? "").Contains(key) ||
                    SlugHelper.NormalizeSearchText(tp.SlugTap ?? "").Contains(key) ||
                    SlugHelper.NormalizeSearchText(tp.TenFile ?? "").Contains(key) ||
                    SlugHelper.NormalizeSearchText(tp.Phim?.TenP ?? "").Contains(key)
                )
                .OrderByDescending(tp => tp.ThoiGianCapNhat)
                .ToList();

            var total = matched.Count;
            var last = (int)Math.Ceiling((double)total / limit);

            var result = matched
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToList();

            return (result, total, last);
        }
    }
}
