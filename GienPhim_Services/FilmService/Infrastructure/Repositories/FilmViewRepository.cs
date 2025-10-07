using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class FilmViewRepository : IFilmViewRepository
    {
        private readonly FilmDbContext _context;

        public FilmViewRepository(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<(List<FilmView> filmViews, int total, int last)> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _context.FilmView
                .Include(fv => fv.Phim)
                .AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var filmViews = await query
                .OrderByDescending(fv => fv.ThoiGianXem)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (filmViews, total, last);
        }

        public async Task<FilmView?> GetByIdAsync(string filmViewId, CancellationToken cancellationToken = default)
        {
            return await _context.FilmView
                .Include(fv => fv.Phim)
                .FirstOrDefaultAsync(fv => fv.FilmViewId == filmViewId, cancellationToken);
        }

        public async Task<List<FilmView>> GetByPhimIdAsync(int phimId, CancellationToken cancellationToken = default)
        {
            return await _context.FilmView
                .Include(fv => fv.Phim)
                .Where(fv => fv.PhimId == phimId)
                .OrderByDescending(fv => fv.ThoiGianXem)
                .ToListAsync(cancellationToken);
        }

        public async Task<FilmView?> GetLastViewByUserAsync(string? userId, int phimId, CancellationToken ct = default)
        {
            return await _context.FilmView
                .Where(v => v.PhimId == phimId && (userId == null || v.UserId == userId))
                .OrderByDescending(v => v.ThoiGianXem)
                .FirstOrDefaultAsync(ct);
        }

        public async Task<int> CountByPhimIdAsync(int phimId, CancellationToken ct = default)
        {
            return await _context.FilmView.CountAsync(v => v.PhimId == phimId, ct);
        }

        public async Task<FilmView> CreateAsync(FilmView filmView, CancellationToken cancellationToken = default)
        {
            await _context.FilmView.AddAsync(filmView, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return filmView;
        }

        public async Task<FilmView> UpdateAsync(FilmView filmView, CancellationToken cancellationToken = default)
        {
            _context.FilmView.Update(filmView);
            await _context.SaveChangesAsync(cancellationToken);
            return filmView;
        }

        public async Task DeleteAsync(FilmView filmView, CancellationToken cancellationToken = default)
        {
            _context.FilmView.Remove(filmView);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
