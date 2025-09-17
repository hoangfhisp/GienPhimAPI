

using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Infrastructure.Data;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FilmDbContext _context;

        public UnitOfWork(FilmDbContext context, IDaoDienRepository daoDien)
        {
            _context = context;
            DaoDiens = daoDien;
        }

        public IDaoDienRepository DaoDiens { get; }

        public async Task<IEfTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            return new EfTransaction(transaction);
        }
    }

}
