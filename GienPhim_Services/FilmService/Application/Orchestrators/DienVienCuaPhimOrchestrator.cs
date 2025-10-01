using GienPhim_Services.FilmService.Application.DTOs.Client.DienVienCuaPhim;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class DienVienCuaPhimOrchestrator : IDienVienCuaPhimOrchestrator
    {
        private readonly IDienVienCuaPhimFactory _factory;
        private readonly IDienVienCuaPhimDomainService _domainService;
        private readonly ILogger<DienVienCuaPhimOrchestrator> _logger;

        public DienVienCuaPhimOrchestrator(
            IDienVienCuaPhimFactory factory,
            IDienVienCuaPhimDomainService domainService,
            ILogger<DienVienCuaPhimOrchestrator> logger)
        {
            _factory = factory;
            _domainService = domainService;
            _logger = logger;
        }

        public async Task<DienVienCuaPhim> CreateAsync(DienVienCuaPhimCreateDTO dto, IEnumerable<DienVienCuaPhim> existing, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateAsync cho quan hệ PhimId={PhimId}, DienVienId={DienVienId}", dto.PhimId, dto.DienVienId);

            var dienVienCuaPhim = _factory.Create(dto);

            _domainService.EnsureCanCreate(dienVienCuaPhim, existing);

            _logger.LogInformation("Orchestrator: Quan hệ PhimId={PhimId}, DienVienId={DienVienId} đã được validate thành công", dto.PhimId, dto.DienVienId);

            return await Task.FromResult(dienVienCuaPhim);
        }

        public async Task<DienVienCuaPhim> UpdateAsync(DienVienCuaPhimUpdateDTO dto, DienVienCuaPhim? existing, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateAsync cho quan hệ PhimId={PhimId}, DienVienId={DienVienId}", dto.PhimId, dto.DienVienId);

            var dienVienCuaPhim = _factory.Update(dto);

            _domainService.EnsureCanUpdate(dienVienCuaPhim, existing);

            _logger.LogInformation("Orchestrator: Quan hệ PhimId={PhimId}, DienVienId={DienVienId} đã được validate update thành công", dto.PhimId, dto.DienVienId);

            return await Task.FromResult(dienVienCuaPhim);
        }

        public async Task DeleteAsync(DienVienCuaPhimDeleteDTO dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteAsync cho quan hệ PhimId={PhimId}, DienVienId={DienVienId}", dto.PhimId, dto.DienVienId);

            var dienVienCuaPhim = new DienVienCuaPhim { PhimId = dto.PhimId, DienVienId = dto.DienVienId };

            _domainService.EnsureCanDelete(dienVienCuaPhim);

            _logger.LogInformation("Orchestrator: Quan hệ PhimId={PhimId}, DienVienId={DienVienId} đã được validate để xóa", dto.PhimId, dto.DienVienId);

            await Task.CompletedTask;
        }
    }
}
