using GienPhim_Services.FilmService.Application.DTOs.Client.TheTag;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class TheTagOrchestrator : ITheTagOrchestrator
    {
        private readonly ITheTagFactory _theTagfactory;
        private readonly ITheTagDomainService _domainService;
        private readonly ILogger<TheTagOrchestrator> _logger;

        public TheTagOrchestrator(
            ITheTagFactory theTagfactory,
            ITheTagDomainService domainService,
            ILogger<TheTagOrchestrator> logger)
        {
            _theTagfactory = theTagfactory;
            _domainService = domainService;
            _logger = logger;
        }

        public async Task<TheTag> CreateTheTagAsync(TheTagCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateTheTagAsync với tên {TenT}", createDto.TenT);

            var tag = _theTagfactory.CreateTheTag(createDto);

            _domainService.EnsureCanCreate(tag);

            _logger.LogInformation("Orchestrator: TheTag {TenT} đã được validate và khởi tạo", createDto.TenT);
            return tag;
        }

        public async Task<TheTag> UpdateTheTagAsync(TheTagUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateTheTagAsync với Id={TheTagId}", updateDto.TheTagId);

            var tag = _theTagfactory.UpdateTheTag(updateDto);

            _domainService.EnsureCanUpdate(tag);

            _logger.LogInformation("Orchestrator: TheTag Id={TheTagId} đã được cập nhật", updateDto.TheTagId);
            return tag;
        }

        public async Task DeleteTheTagAsync(TheTagDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteTheTagAsync với Id={TheTagId}", deleteDto.TheTagId);

            var tag = _theTagfactory.DeleteTheTag(deleteDto);

            _domainService.EnsureCanDelete(tag);

            _logger.LogInformation("Orchestrator: TheTag Id={TheTagId} đã được xóa", deleteDto.TheTagId);
        }
    }
}
