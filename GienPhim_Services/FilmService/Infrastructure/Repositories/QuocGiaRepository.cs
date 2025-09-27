using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class QuocGiaRepository : IQuocGiaRepository
    {
        private readonly FilmDbContext _filmContext;

        public QuocGiaRepository(FilmDbContext filmContext)
        {
            _filmContext = filmContext;
        }

        public async Task<List<QuocGia>> GetAllQuocGiasAsync(CancellationToken cancellationToken = default)
        {
            return await _filmContext.QuocGia.ToListAsync(cancellationToken);
        }

        public async Task<QuocGia?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _filmContext.QuocGia
                .FirstOrDefaultAsync(x => x.QuocGiaId == id, ct);
        }

        public async Task<QuocGia> CreateAsync(QuocGia e, CancellationToken ct = default)
        {
            _filmContext.QuocGia.Add(e);
            await _filmContext.SaveChangesAsync(ct);
            return e;
        }

        public async Task<QuocGia> UpdateAsync(QuocGia e, CancellationToken ct = default)
        {
            _filmContext.QuocGia.Update(e);
            await _filmContext.SaveChangesAsync(ct);
            return e;
        }

        public async Task DeleteAsync(QuocGia e, CancellationToken ct = default)
        {
            _filmContext.QuocGia.Remove(e);
            await _filmContext.SaveChangesAsync(ct);
        }
    }
}
