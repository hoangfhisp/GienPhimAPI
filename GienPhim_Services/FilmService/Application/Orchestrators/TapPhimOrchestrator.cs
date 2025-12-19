using GienPhim_Services.FilmService.Application.DTOs.Client.TapPhim;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class TapPhimOrchestrator : ITapPhimOrchestrator
    {
        private readonly ITapPhimFactory _tapPhimFactory;
        private readonly ITapPhimDomainService _tapPhimDomainService;
        private readonly ILogger<TapPhimOrchestrator> _logger;

        public TapPhimOrchestrator(
            ITapPhimFactory tapPhimFactory,
            ITapPhimDomainService tapPhimDomainService,
            ILogger<TapPhimOrchestrator> logger)
        {
            _tapPhimFactory = tapPhimFactory;
            _tapPhimDomainService = tapPhimDomainService;
            _logger = logger;
        }

        public async Task<TapPhim> CreateTapPhimAsync(TapPhimCreateDTO createDto, string? linkChinh, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateTapPhimAsync cho PhimId={PhimId}, TenTap={TenTap}", createDto.PhimId, createDto.TenTap);

            // Factory -> Entity
            var tapPhim = _tapPhimFactory.Create(createDto, linkChinh);

            // Domain Service check
            _tapPhimDomainService.EnsureCanCreate(tapPhim, Enumerable.Empty<TapPhim>());

            _logger.LogInformation("Orchestrator: Tập phim {TenTap} đã được validate và khởi tạo thành công", createDto.TenTap);

            return tapPhim;
        }

        public async Task<TapPhim> UpdateTapPhimAsync(TapPhimUpdateDTO updateDto, string? linkChinh, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateTapPhimAsync Id={TapId}", updateDto.TapId);

            // Factory -> Entity
            var tapPhim = _tapPhimFactory.Update(updateDto, linkChinh);

            // Domain Service check
            _tapPhimDomainService.EnsureCanUpdate(tapPhim, null); // existing sẽ được lấy ở Service

            _logger.LogInformation("Orchestrator: Tập phim Id={TapId} đã được validate để cập nhật", updateDto.TapId);

            return tapPhim;
        }

        public async Task DeleteTapPhimAsync(TapPhimDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteTapPhimAsync Id={TapId}", deleteDto.TapId);

            var tapPhim = new TapPhim { TapId = deleteDto.TapId };

            _tapPhimDomainService.EnsureCanDelete(tapPhim);

            _logger.LogInformation("Orchestrator: Tập phim Id={TapId} đã được validate để xoá", deleteDto.TapId);
        }
    }
}
