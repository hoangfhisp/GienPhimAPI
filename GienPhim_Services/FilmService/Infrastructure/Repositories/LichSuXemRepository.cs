using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using System.Threading;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class LichSuXemRepository : ILichSuXemRepository
    {
        private readonly FilmDbContext _context;

        public LichSuXemRepository(FilmDbContext context)
        {
            _context = context;
        }

        // Lấy lịch sử xem của người dùng
        public async Task<(List<LichSuXem> lichSuXems, int total, int last)> GetUserLichSuXem(string UserId, int page, int limit, CancellationToken cancellationToken = default) 
        {
            var query = _context.LichSuXem
                .Where(l => l.UserId == UserId)
            .Include(l => l.Phim)
            .OrderByDescending(l => l.ThoiGianXem)
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

        // Thêm lịch sử xem
        public async Task<LichSuXem> AddLichSuXemAsync(LichSuXem lichSuXem)
        {
            await _context.LichSuXem.AddAsync(lichSuXem);
            await _context.SaveChangesAsync();
            return lichSuXem;
        }

        // Cập nhật lịch sử xem
        public async Task<LichSuXem> UpdateLichSuXemAsync(LichSuXem lichSuXem)
        {
            _context.LichSuXem.Update(lichSuXem);
            await _context.SaveChangesAsync();
            return lichSuXem;
        }

        // Kiểm tra lịch sử xem
        public async Task<LichSuXem> CheckLichSuXemAsync(int IdPhim, string UserId)
        {
            return await _context.LichSuXem.FirstOrDefaultAsync(l => l.PhimId == IdPhim && l.UserId == UserId);
        }
    }
}
