using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class PhimOrchestrator : IPhimOrchestrator
    {
        private readonly IPhimFactory _phimFactory;
        private readonly IPhimDomainService _phimDomainService;
        private readonly ILogger<PhimOrchestrator> _logger;

        public PhimOrchestrator(
            IPhimFactory phimFactory,
            IPhimDomainService phimDomainService,
            ILogger<PhimOrchestrator> logger)
        {
            _phimFactory = phimFactory;
            _phimDomainService = phimDomainService;
            _logger = logger;
        }

        public async Task<Phim> CreatePhimAsync(PhimCreateDTO createDto, string avatar, string poster, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreatePhimAsync với tên {TenP}", createDto.TenP);

            // Factory -> Entity
            var phim = _phimFactory.CreatePhim(createDto, avatar, poster);

            // Domain Service check
            _phimDomainService.EnsureCanCreate(phim, avatar, poster);

            _logger.LogInformation("Orchestrator: Phim {TenP} đã được validate và khởi tạo thành công", createDto.TenP);

            return phim;
        }

        public async Task<Phim> UpdatePhimAsync(PhimUpdateDTO updateDto, string? avatar, string? poster, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdatePhimAsync Id={PhimId}", updateDto.PhimId);

            // Factory -> Entity
            var phim = _phimFactory.UpdatePhim(updateDto, avatar, poster);

            // Domain Service check
            _phimDomainService.EnsureCanUpdate(phim, avatar, poster);

            _logger.LogInformation("Orchestrator: Phim Id={PhimId} đã được cập nhật thành công", updateDto.PhimId);

            return phim;
        }

        public async Task DeletePhimAsync(PhimDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeletePhimAsync Id={PhimId}", deleteDto.PhimId);

            var phim = new Phim { PhimId = deleteDto.PhimId };

            _phimDomainService.EnsureCanDelete(phim);

            _logger.LogInformation("Orchestrator: Phim Id={PhimId} đã được validate để xoá", deleteDto.PhimId);
        }
    }
}
