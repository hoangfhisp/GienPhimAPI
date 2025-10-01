using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheTag;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class PhanLoaiTheTagOrchestrator : IPhanLoaiTheTagOrchestrator
    {
        private readonly IPhanLoaiTheTagFactory _factory;
        private readonly IPhanLoaiTheTagDomainService _domainService;
        private readonly ILogger<PhanLoaiTheTagOrchestrator> _logger;

        public PhanLoaiTheTagOrchestrator(
            IPhanLoaiTheTagFactory factory,
            IPhanLoaiTheTagDomainService domainService,
            ILogger<PhanLoaiTheTagOrchestrator> logger)
        {
            _factory = factory;
            _domainService = domainService;
            _logger = logger;
        }

        public async Task<PhanLoaiTheTag> CreateAsync(PhanLoaiTheTagCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateAsync cho PhimId={PhimId}, TheTagId={TheTagId}", createDto.PhimId, createDto.TheTagId);

            var phanLoaiTheTag = _factory.Create(createDto);

            // Validate bằng DomainService (chỗ này existing list sẽ check ở Service)
            _domainService.EnsureCanCreate(phanLoaiTheTag, new List<PhanLoaiTheTag>());

            _logger.LogInformation("Orchestrator: Quan hệ PhimId={PhimId}, TheTagId={TheTagId} đã được validate", phanLoaiTheTag.PhimId, phanLoaiTheTag.TheTagId);

            return await Task.FromResult(phanLoaiTheTag);
        }

        public async Task<PhanLoaiTheTag> UpdateAsync(PhanLoaiTheTagUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateAsync cho PhimId={PhimId}, TheTagId={TheTagId}", updateDto.PhimId, updateDto.TheTagId);

            var phanLoaiTheTag = _factory.Update(updateDto);

            _domainService.EnsureCanUpdate(phanLoaiTheTag, phanLoaiTheTag); // existing sẽ truyền từ Service

            _logger.LogInformation("Orchestrator: Quan hệ PhimId={PhimId}, TheTagId={TheTagId} đã được validate update", phanLoaiTheTag.PhimId, phanLoaiTheTag.TheTagId);

            return await Task.FromResult(phanLoaiTheTag);
        }

        public async Task DeleteAsync(PhanLoaiTheTagDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteAsync cho PhimId={PhimId}, TheTagId={TheTagId}", deleteDto.PhimId, deleteDto.TheTagId);

            var phanLoaiTheTag = _factory.Delete(deleteDto);

            _domainService.EnsureCanDelete(phanLoaiTheTag); // check null sẽ được handle ở Service

            _logger.LogInformation("Orchestrator: Quan hệ PhimId={PhimId}, TheTagId={TheTagId} đã được validate delete", phanLoaiTheTag.PhimId, phanLoaiTheTag.TheTagId);

            await Task.CompletedTask;
        }
    }
}
