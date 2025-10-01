namespace GienPhim_Services.FilmService.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IDaoDienRepository DaoDiens { get; } // Repository
        IDienVienRepository DienViens { get; } // Repository
        ITheLoaiRepository TheLoais { get; } // Repository
        ITheTagRepository TheTags { get; } // Repository
        INgonNguRepository NgonNgus { get; } // Repository
        IQuocGiaRepository QuocGias { get; } // Repository
        IPhimRepository Phims { get; } // Repository 
        IDaoDienCuaPhimRepository DaoDienCuaPhims { get; } // Repository
        IDienVienCuaPhimRepository DienVienCuaPhims { get; } // Repository
        IPhanLoaiTheLoaiRepository PhanLoaiTheLoais { get; } // Repository
        IPhanLoaiTheTagRepository PhanLoaiTheTags { get; } // Repository
        Task<IEfTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default); // Transaction
    }
}
