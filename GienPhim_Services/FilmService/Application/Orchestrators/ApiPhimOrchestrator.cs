using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Infrastructure.Data.HttpClients.Interfaces;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;
using Shared.FileStorage;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class ApiPhimOrchestrator : IApiPhimOrchestrator
    {
        private readonly IApiPhimServiceClient _apiClient;
        private readonly IApiPhimFactory _factory;
        private readonly IPhimDomainService _domainService;
        private readonly ILogger<ApiPhimOrchestrator> _logger;
        private readonly IApiPhimMapper _mapper;
        private readonly INgonNguRepository _ngonNguRepository;
        private readonly IQuocGiaRepository _quocGiaRepository;
        private readonly IDienVienRepository _dienVienRepository;
        private readonly IDaoDienRepository _daoDienRepository;
        private readonly ITheLoaiRepository _theLoaiRepository;

        public ApiPhimOrchestrator(
            IApiPhimServiceClient apiClient,
            IApiPhimFactory factory,
            IPhimDomainService domainService,
            IApiPhimMapper mapper,
            ILogger<ApiPhimOrchestrator> logger,
            INgonNguRepository ngonNguRepository,
            IQuocGiaRepository quocGiaRepository,
            IDienVienRepository dienVienRepository,
            IDaoDienRepository daoDienRepository,
            ITheLoaiRepository theLoaiRepository)
        {
            _apiClient = apiClient;
            _factory = factory;
            _domainService = domainService;
            _mapper = mapper;
            _logger = logger;
            _ngonNguRepository = ngonNguRepository;
            _quocGiaRepository = quocGiaRepository;
            _dienVienRepository = dienVienRepository;
            _daoDienRepository = daoDienRepository;
            _theLoaiRepository = theLoaiRepository;
        }

        public async Task<Phim> ProcessImportAsync(string slug, CancellationToken ct = default)
        {
            _logger.LogInformation("Orchestrator: Gọi API lấy dữ liệu phim {Slug}", slug);

            // 1️ Gọi API ngoài
            var dto = await _apiClient.GetPhimDataAsync(slug, ct);
            if (dto == null || dto.Movie == null)
                throw new InvalidOperationException($"Không thể lấy dữ liệu phim từ API với slug {slug}");

            var ngonNguDraft = _factory.CreateNgonNgu(dto);

            var ngonNgu = await _ngonNguRepository
                .GetByMoTaNNAsync(ngonNguDraft.MoTaNN, ct);

            if (ngonNgu == null)
            {
                ngonNgu = ngonNguDraft;
            }
            var quocGiaDraft = _factory.CreateQuocGia(dto);

            var quocGia = await _quocGiaRepository.GetByMoTaQGAsync(quocGiaDraft.MoTaQG, ct);
            if (quocGia == null)
            {
                quocGia = quocGiaDraft;
            }

            var phim = _factory.CreatePhim(dto, ngonNgu, quocGia, slug);

            // 3️ Validate domain rule (đảm bảo phim hợp lệ)
            _domainService.EnsureCanCreateApiFilm(phim);

            // 4️ Dựng các danh sách phụ (thuộc aggregate Phim)
            // ------------------ THỂ LOẠI ------------------
            var theLoaiDrafts = _factory.CreateTheLoais(dto);

            // 1) Lấy key (MoTa = slug) và loại trùng trong list API
            var keys = theLoaiDrafts
                .Where(t => !string.IsNullOrWhiteSpace(t.MoTa))
                .Select(t => t.MoTa!.Trim())
                .Distinct()
                .ToList();

            // 2) Query DB 1 lần lấy tất cả thể loại đã có
            var existing = await _theLoaiRepository.GetByMoTasAsync(keys, ct);
            var map = existing.ToDictionary(t => t.MoTa!.Trim(), t => t);

            // 3) Tạo những cái chưa có (và add vào map)
            foreach (var draft in theLoaiDrafts)
            {
                var key = draft.MoTa?.Trim();
                if (string.IsNullOrWhiteSpace(key)) continue;

                if (!map.ContainsKey(key))
                {
                    draft.MoTa = key;
                    map[key] = draft;
                }
            }

            // 4) Attach vào phim (join table)
            phim.PhanLoaiTheLoais = keys.Select(k => map[k]).Select(tl => new PhanLoaiTheLoai
            {
                TheLoai = tl,
                Phim = phim,
                ThoiGianTao = DateTime.UtcNow,
                ThoiGianCapNhat = DateTime.UtcNow
            }).ToList();

            // ------------------ DIỄN VIÊN ------------------
            var dienVienDrafts = _factory.CreateDienViens(dto);

            // (khuyên) loại trùng ngay trong list API
            dienVienDrafts = dienVienDrafts
                .Where(d => !string.IsNullOrWhiteSpace(d.MoTaDV ?? d.TenDV))
                .GroupBy(d => SlugHelper.NormalizeSearchText(d.MoTaDV ?? d.TenDV ?? ""))
                .Select(g => g.First())
                .ToList();

            var dienVienLinks = new List<DienVienCuaPhim>();

            foreach (var dvDraft in dienVienDrafts)
            {
                var key = dvDraft.MoTaDV ?? dvDraft.TenDV ?? "";
                var dv = await _dienVienRepository.GetByMoTaDVAsync(key, ct);

                if (dv == null)
                    dv = dvDraft;

                dienVienLinks.Add(new DienVienCuaPhim
                {
                    DienVien = dv,
                    Phim = phim,
                    ThoiGianTao = DateTime.UtcNow,
                    ThoiGianCapNhat = DateTime.UtcNow
                });
            }

            phim.DienVienCuaPhims = dienVienLinks;

            // ------------------ ĐẠO DIỄN ------------------
            var daoDienDrafts = _factory.CreateDaoDiens(dto);

            // (khuyên) loại trùng ngay trong list API
            daoDienDrafts = daoDienDrafts
                .Where(d => !string.IsNullOrWhiteSpace(d.MoTaDD ?? d.TenDD))
                .GroupBy(d => SlugHelper.NormalizeSearchText(d.MoTaDD ?? d.TenDD ?? ""))
                .Select(g => g.First())
                .ToList();

            var daoDienLinks = new List<DaoDienCuaPhim>();

            foreach (var ddDraft in daoDienDrafts)
            {
                var key = ddDraft.MoTaDD ?? ddDraft.TenDD ?? "";
                var dd = await _daoDienRepository.GetByMoTaDDAsync(key, ct);

                if (dd == null)
                    dd = ddDraft;

                daoDienLinks.Add(new DaoDienCuaPhim
                {
                    DaoDien = dd,
                    Phim = phim,
                    VaiTroDG = "Đang cập nhật",
                    ThoiGianTao = DateTime.UtcNow,
                    ThoiGianCapNhat = DateTime.UtcNow
                });
            }

            phim.DaoDienCuaPhims = daoDienLinks;

            // ------------------ TẬP PHIM ------------------
            phim.TapPhims = _factory.CreateTapPhims(dto, phim.PhimId);

            _logger.LogInformation("Orchestrator: Hoàn tất ánh xạ phim {TenP}", phim.TenP);

            return phim;
        }

        public async Task<Phim> ProcessUpdateAsync(string slug, CancellationToken ct = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu ProcessUpdateAsync cho phim {Slug}", slug);

            // 1️ Lấy dữ liệu mới từ API ngoài
            var dto = await _apiClient.GetPhimDataAsync(slug, ct);
            if (dto == null || dto.Movie == null)
                throw new InvalidOperationException($"Không thể lấy dữ liệu phim từ API với slug {slug}");

            // 2️ Lấy entity hiện có trong DB
            var existing = await _factory.GetExistingPhimAsync(slug, ct);
            // Nếu bạn chưa có hàm này trong factory, có thể chuyển sang lấy trực tiếp qua UnitOfWork.Phims trong Service

            if (existing == null)
                throw new InvalidOperationException($"Phim slug={slug} chưa tồn tại trong hệ thống.");

            // 3️ Cập nhật thông tin cơ bản (poster, trailer, lượt xem,…)
            _factory.UpdatePhim(existing, dto);

            // 4️ Cập nhật danh sách tập phim (thêm tập mới hoặc cập nhật link)
            _factory.UpdateTapPhims(existing, dto);

            // 5️ Validate domain rule (nếu có quy định phim bị khoá không cho update,…)
            _domainService.EnsureCanUpdateApiFilm(existing);

            _logger.LogInformation("Orchestrator: Hoàn tất cập nhật phim {TenP}", existing.TenP);

            return existing;
        }
    }
}
