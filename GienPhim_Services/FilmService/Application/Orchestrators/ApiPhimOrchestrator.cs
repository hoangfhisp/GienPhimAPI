using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Infrastructure.Data.HttpClients.Interfaces;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class ApiPhimOrchestrator : IApiPhimOrchestrator
    {
        private readonly IApiPhimServiceClient _apiClient;
        private readonly IApiPhimFactory _factory;
        private readonly IPhimDomainService _domainService;
        private readonly ILogger<ApiPhimOrchestrator> _logger;
        private readonly IApiPhimMapper _mapper;

        public ApiPhimOrchestrator(
            IApiPhimServiceClient apiClient,
            IApiPhimFactory factory,
            IPhimDomainService domainService,
            IApiPhimMapper mapper,
            ILogger<ApiPhimOrchestrator> logger)
        {
            _apiClient = apiClient;
            _factory = factory;
            _domainService = domainService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Phim> ProcessImportAsync(string slug, CancellationToken ct = default)
        {
            _logger.LogInformation("Orchestrator: Gọi API lấy dữ liệu phim {Slug}", slug);

            // 1️⃣ Gọi API ngoài
            var dto = await _apiClient.GetPhimDataAsync(slug, ct);
            if (dto == null || dto.Movie == null)
                throw new InvalidOperationException($"Không thể lấy dữ liệu phim từ API với slug {slug}");

            // 2️⃣ Dùng factory để tạo các entity gốc (Aggregate Root)
            var ngonNgu = _factory.CreateNgonNgu(dto);
            var quocGia = _factory.CreateQuocGia(dto);
            var phim = _factory.CreatePhim(dto, ngonNgu, quocGia, slug);

            // 3️⃣ Validate domain rule (đảm bảo phim hợp lệ)
            _domainService.EnsureCanCreateApiFilm(phim);

            // 4️⃣ Dựng các danh sách phụ (thuộc aggregate Phim)
            // ------------------ THỂ LOẠI ------------------
            var theLoais = _factory.CreateTheLoais(dto);
            phim.PhanLoaiTheLoais = theLoais.Select(tl => new PhanLoaiTheLoai
            {
                TheLoai = tl,                      // ✅ Gán navigation
                Phim = phim,                       // ✅ Gán navigation
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            }).ToList();

            // ------------------ DIỄN VIÊN ------------------
            var dienViens = _factory.CreateDienViens(dto);
            phim.DienVienCuaPhims = dienViens.Select(dv => new DienVienCuaPhim
            {
                DienVien = dv,
                Phim = phim,
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            }).ToList();

            // ------------------ ĐẠO DIỄN ------------------
            var daoDiens = _factory.CreateDaoDiens(dto);
            phim.DaoDienCuaPhims = daoDiens.Select(dd => new DaoDienCuaPhim
            {
                DaoDien = dd,
                Phim = phim,
                VaiTroDG = "Đang cập nhật",
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            }).ToList();

            // ------------------ TẬP PHIM ------------------
            phim.TapPhims = _factory.CreateTapPhims(dto, phim.PhimId);

            _logger.LogInformation("Orchestrator: Hoàn tất ánh xạ phim {TenP}", phim.TenP);

            return phim;
        }
    }
}
