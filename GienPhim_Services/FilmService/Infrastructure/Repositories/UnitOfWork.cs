using GienPhim_Services.FilmService.Application.Interfaces.HttpClients;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Infrastructure.Data;
using GienPhim_Services.FilmService.Infrastructure.Data.HttpClients.Interfaces;

namespace GienPhim_Services.FilmService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FilmDbContext _context;

        public UnitOfWork
        (
            FilmDbContext context,
            IDaoDienRepository daoDien,
            IDienVienRepository dienVien,
            ITheLoaiRepository theLoai,
            ITheTagRepository TheTag,
            INgonNguRepository ngonNgu,
            IQuocGiaRepository quocGia,
            IPhimRepository phim,
            IDaoDienCuaPhimRepository daoDienCuaPhim,
            IDienVienCuaPhimRepository dienVienCuaPhim,
            IPhanLoaiTheLoaiRepository phanLoaiTheLoai,
            IPhanLoaiTheTagRepository phanLoaiTheTags,
            ITapPhimRepository tapPhims,
            IFilmViewRepository filmViews,
            ILichSuXemRepository lichSuXem,
            IBotConfigRepository botConfig)
        {
            _context = context;
            DaoDiens = daoDien;
            DienViens = dienVien;
            TheLoais = theLoai;
            TheTags = TheTag;
            NgonNgus = ngonNgu;
            QuocGias = quocGia;
            Phims = phim;
            DaoDienCuaPhims = daoDienCuaPhim;
            DienVienCuaPhims = dienVienCuaPhim;
            PhanLoaiTheLoais = phanLoaiTheLoai;
            PhanLoaiTheTags = phanLoaiTheTags;
            TapPhims = tapPhims;
            FilmViews = filmViews;
            LichSuXem = lichSuXem;
            BotConfigs = botConfig;
        }

        public IDaoDienRepository DaoDiens { get; }
        public IDienVienRepository DienViens { get; }
        public ITheLoaiRepository TheLoais { get; }
        public ITheTagRepository TheTags { get; }
        public INgonNguRepository NgonNgus { get; }
        public IQuocGiaRepository QuocGias { get; }
        public IPhimRepository Phims { get; }
        public IDaoDienCuaPhimRepository DaoDienCuaPhims { get; }
        public IDienVienCuaPhimRepository DienVienCuaPhims { get; }
        public IPhanLoaiTheLoaiRepository PhanLoaiTheLoais { get; }
        public IPhanLoaiTheTagRepository PhanLoaiTheTags { get; }
        public ITapPhimRepository TapPhims { get; }
        public IFilmViewRepository FilmViews { get; }
        public IExternalApiClient ExternalApiClients { get; }
        public ILichSuXemRepository LichSuXem { get; }
        public IBotConfigRepository BotConfigs { get; }

        public async Task<IEfTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            return new EfTransaction(transaction);
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

    }

}
