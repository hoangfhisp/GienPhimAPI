using GienPhim_Services.FilmService.Application.DTOs.Client.QuocGia;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Validators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class QuocGiaOrchestrator : IQuocGiaOrchestrator
    {
        private readonly IQuocGiaFactory _quocGiaFactory;
        private readonly IValidationService _validationService;
        private readonly IQuocGiaDomainService _quocGiaDomainService;
        private readonly ILogger<QuocGiaOrchestrator> _logger;

        public QuocGiaOrchestrator(
            IQuocGiaFactory quocGiaFactory,
            IValidationService validationService,
            IQuocGiaDomainService quocGiaDomainService,
            ILogger<QuocGiaOrchestrator> logger)
        {
            _quocGiaFactory = quocGiaFactory;
            _validationService = validationService;
            _quocGiaDomainService = quocGiaDomainService;
            _logger = logger;
        }

        public async Task<QuocGia> CreateQuocGiaAsync(QuocGiaCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateQuocGiaAsync với tên quốc gia {TenQG}", createDto.TenQG);

            await _validationService.ValidateAsync(createDto, cancellationToken);

            var quocGia = _quocGiaFactory.CreateQuocGia(createDto);

            _quocGiaDomainService.EnsureCanCreate(quocGia);

            _logger.LogInformation("Orchestrator: Quốc gia {TenQG} đã được validate và khởi tạo thành công", createDto.TenQG);
            return quocGia;
        }

        public async Task<QuocGia> UpdateQuocGiaAsync(QuocGiaUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateQuocGiaAsync với Id={QuocGiaId}", updateDto.QuocGiaId);

            await _validationService.ValidateAsync(updateDto, cancellationToken);

            var quocGia = _quocGiaFactory.UpdateQuocGia(updateDto);

            _quocGiaDomainService.EnsureCanUpdate(quocGia);

            _logger.LogInformation("Orchestrator: Quốc gia Id={QuocGiaId} đã được cập nhật thành công", updateDto.QuocGiaId);
            return quocGia;
        }

        public async Task DeleteQuocGiaAsync(QuocGiaDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteQuocGiaAsync với Id={QuocGiaId}", deleteDto.QuocGiaId);

            await _validationService.ValidateAsync(deleteDto, cancellationToken);

            var quocGia = _quocGiaFactory.DeleteQuocGia(deleteDto);

            _quocGiaDomainService.EnsureCanDelete(quocGia);

            _logger.LogInformation("Orchestrator: Quốc gia Id={QuocGiaId} đã được xoá thành công", deleteDto.QuocGiaId);
        }
    }
}
