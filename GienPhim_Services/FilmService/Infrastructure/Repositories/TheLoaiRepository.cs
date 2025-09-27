using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class TheLoaiRepository : ITheLoaiRepository
    {
        private readonly FilmDbContext _filmContext;

        public TheLoaiRepository(FilmDbContext filmContext)
        {
            _filmContext = filmContext;
        }

        // Lấy tất cả thể loại
        public async Task<List<TheLoai>> GetAllTheLoaisAsync(CancellationToken cancellationToken = default)
        {
            return await _filmContext.TheLoai.ToListAsync(cancellationToken);
        }

        public async Task<TheLoai?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _filmContext.TheLoai
                .FirstOrDefaultAsync(x => x.TheLoaiId == id, ct);
        }

        public async Task<TheLoai> CreateAsync(TheLoai e, CancellationToken ct = default)
        {
            _filmContext.TheLoai.Add(e);
            await _filmContext.SaveChangesAsync(ct);
            return e;
        }

        public async Task<TheLoai> UpdateAsync(TheLoai e, CancellationToken ct = default)
        {
            _filmContext.TheLoai.Update(e);
            await _filmContext.SaveChangesAsync(ct);
            return e;
        }

        public async Task DeleteAsync(TheLoai e, CancellationToken ct = default)
        {
            _filmContext.TheLoai.Remove(e);
            await _filmContext.SaveChangesAsync(ct);
        }
    }
}
