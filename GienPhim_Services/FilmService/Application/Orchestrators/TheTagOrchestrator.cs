using GienPhim_Services.FilmService.Application.DTOs.Client.TheTag;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Validators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class TheTagOrchestrator : ITheTagOrchestrator
    {
        private readonly ITheTagFactory _factory;
        private readonly IValidationService _validationService;
        private readonly ITheTagDomainService _domainService;
        private readonly ITheTagRepository _repository;
        private readonly ILogger<TheTagOrchestrator> _logger;

        public TheTagOrchestrator(
            ITheTagFactory factory,
            IValidationService validationService,
            ITheTagDomainService domainService,
            ITheTagRepository repository,
            ILogger<TheTagOrchestrator> logger)
        {
            _factory = factory;
            _validationService = validationService;
            _repository = repository;
            _domainService = domainService;
            _logger = logger;
        }

        public async Task<IEnumerable<TheTag>> GetAllTheTagsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu GetAllTheTagsAsync");

            var tags = await _repository.GetAllTheTagsAsync(cancellationToken);

            _logger.LogInformation("Orchestrator: Lấy danh sách TheTag thành công, tổng={Count}", tags.Count);

            return tags;
        }

        public async Task<TheTag> CreateTheTagAsync(TheTagCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateTheTagAsync với tên {TenT}", createDto.TenT);

            await _validationService.ValidateAsync(createDto, cancellationToken);

            var tag = _factory.CreateTheTag(createDto);

            _domainService.EnsureCanCreate(tag);

            _logger.LogInformation("Orchestrator: TheTag {TenT} đã được validate và khởi tạo", createDto.TenT);
            return tag;
        }

        public async Task<TheTag> UpdateTheTagAsync(TheTagUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateTheTagAsync với Id={TheTagId}", updateDto.TheTagId);

            await _validationService.ValidateAsync(updateDto, cancellationToken);

            var tag = _factory.UpdateTheTag(updateDto);

            _domainService.EnsureCanUpdate(tag);

            _logger.LogInformation("Orchestrator: TheTag Id={TheTagId} đã được cập nhật", updateDto.TheTagId);
            return tag;
        }

        public async Task DeleteTheTagAsync(TheTagDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteTheTagAsync với Id={TheTagId}", deleteDto.TheTagId);

            await _validationService.ValidateAsync(deleteDto, cancellationToken);

            var tag = _factory.DeleteTheTag(deleteDto);

            _domainService.EnsureCanDelete(tag);

            _logger.LogInformation("Orchestrator: TheTag Id={TheTagId} đã được xóa", deleteDto.TheTagId);
        }
    }
}
