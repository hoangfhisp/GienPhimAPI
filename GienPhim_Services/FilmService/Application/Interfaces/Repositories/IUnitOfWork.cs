namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IDaoDienRepository DaoDiens { get; } // Repository
        IDienVienRepository DienViens { get; } // Repository
        ITheLoaiRepository TheLoais { get; } // Repository
        Task<IEfTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default); // Transaction
    }
}
