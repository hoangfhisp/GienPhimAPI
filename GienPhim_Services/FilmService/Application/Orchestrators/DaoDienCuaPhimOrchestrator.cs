using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDienCuaPhim;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class DaoDienCuaPhimOrchestrator : IDaoDienCuaPhimOrchestrator
    {
        private readonly IDaoDienCuaPhimFactory _factory;
        private readonly IDaoDienCuaPhimDomainService _domainService;
        private readonly ILogger<DaoDienCuaPhimOrchestrator> _logger;

        public DaoDienCuaPhimOrchestrator(
            IDaoDienCuaPhimFactory factory,
            IDaoDienCuaPhimDomainService domainService,
            ILogger<DaoDienCuaPhimOrchestrator> logger)
        {
            _factory = factory;
            _domainService = domainService;
            _logger = logger;
        }

        public async Task<DaoDienCuaPhim> CreateAsync(
            DaoDienCuaPhimCreateDTO createDto,
            IEnumerable<DaoDienCuaPhim> existingRelations,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateAsync cho PhimId={PhimId}, DaoDienId={DaoDienId}",
                createDto.PhimId, createDto.DaoDienId);

            var entity = _factory.Create(createDto);

            _domainService.EnsureCanCreate(entity, existingRelations);

            _logger.LogInformation("Orchestrator: Quan hệ PhimId={PhimId}, DaoDienId={DaoDienId} đã validate OK",
                createDto.PhimId, createDto.DaoDienId);

            return entity;
        }

        public async Task<DaoDienCuaPhim> UpdateAsync(
            DaoDienCuaPhimUpdateDTO updateDto,
            DaoDienCuaPhim? existingRelation,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateAsync cho PhimId={PhimId}, DaoDienId={DaoDienId}",
                updateDto.PhimId, updateDto.DaoDienId);

            var entity = _factory.Update(updateDto);

            _domainService.EnsureCanUpdate(entity, existingRelation);

            _logger.LogInformation("Orchestrator: Quan hệ PhimId={PhimId}, DaoDienId={DaoDienId} đã cập nhật OK",
                updateDto.PhimId, updateDto.DaoDienId);

            return entity;
        }

        public async Task DeleteAsync(
            DaoDienCuaPhimDeleteDTO deleteDto,
            DaoDienCuaPhim? existingRelation,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteAsync cho PhimId={PhimId}, DaoDienId={DaoDienId}",
                deleteDto.PhimId, deleteDto.DaoDienId);

            var entity = _factory.Delete(deleteDto);

            _domainService.EnsureCanDelete(existingRelation);

            _logger.LogInformation("Orchestrator: Quan hệ PhimId={PhimId}, DaoDienId={DaoDienId} đã validate để xoá",
                deleteDto.PhimId, deleteDto.DaoDienId);
        }
    }
}
