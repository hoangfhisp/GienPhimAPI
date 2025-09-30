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

        public async Task<(List<QuocGia> quocGias, int total, int last)> GetAllQuocGiasAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            var query = _filmContext.QuocGia.AsQueryable();

            var total = await query.CountAsync(cancellationToken);
            var last = (int)Math.Ceiling((double)total / limit);

            var quocGias = await query
                .OrderBy(qg => qg.QuocGiaId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return (quocGias, total, last);
        }

        public async Task<QuocGia?> GetByIdAsync(int quocGiaId, CancellationToken ct = default)
        {
            return await _filmContext.QuocGia
                .FirstOrDefaultAsync(qg => qg.QuocGiaId == quocGiaId, ct);
        }

        public async Task<QuocGia> CreateAsync(QuocGia quocGia, CancellationToken ct = default)
        {
            _filmContext.QuocGia.Add(quocGia);
            await _filmContext.SaveChangesAsync(ct);
            return quocGia;
        }

        public async Task<QuocGia> UpdateAsync(QuocGia quocGia, CancellationToken ct = default)
        {
            _filmContext.QuocGia.Update(quocGia);
            await _filmContext.SaveChangesAsync(ct);
            return quocGia;
        }

        public async Task DeleteAsync(QuocGia quocGia, CancellationToken ct = default)
        {
            _filmContext.QuocGia.Remove(quocGia);
            await _filmContext.SaveChangesAsync(ct);
        }
    }
}
