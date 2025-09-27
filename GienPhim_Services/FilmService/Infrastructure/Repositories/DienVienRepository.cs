using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class DienVienRepository : IDienVienRepository
    {
        private readonly FilmDbContext _filmContext;

        public DienVienRepository(FilmDbContext filmContext)
        {
            _filmContext = filmContext;
        }

        public async Task<List<DienVien>> GetAllDienViensAsync(CancellationToken cancellationToken = default)
        {
            return await _filmContext.DienVien.ToListAsync(cancellationToken);
        }

        public async Task<DienVien?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _filmContext.DienVien
                .FirstOrDefaultAsync(x => x.DienVienId == id, ct);
        }

        public async Task<DienVien> CreateAsync(DienVien e, CancellationToken ct = default)
        {
            _filmContext.DienVien.Add(e);
            await _filmContext.SaveChangesAsync(ct);
            return e;
        }

        public async Task<DienVien> UpdateAsync(DienVien e, CancellationToken ct = default)
        {
            _filmContext.DienVien.Update(e);
            await _filmContext.SaveChangesAsync(ct);
            return e;
        }

        public async Task DeleteAsync(DienVien e, CancellationToken ct = default)
        {
            _filmContext.DienVien.Remove(e);
            await _filmContext.SaveChangesAsync(ct);
        }
    }
}
