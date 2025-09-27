using GienPhim_Services.FilmService.Application.DTOs.Client.NgonNgu;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Validators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class NgonNguOrchestrator : INgonNguOrchestrator
    {
        private readonly INgonNguFactory _ngonNguFactory;
        private readonly IValidationService _validationService;
        private readonly INgonNguDomainService _ngonNguDomainService;
        private readonly INgonNguRepository _ngonNguRepository;
        private readonly ILogger<NgonNguOrchestrator> _logger;

        public NgonNguOrchestrator(
            INgonNguFactory ngonNguFactory,
            IValidationService validationService,
            INgonNguDomainService ngonNguDomainService,
            INgonNguRepository ngonNguRepository,
            ILogger<NgonNguOrchestrator> logger)
        {
            _ngonNguFactory = ngonNguFactory;
            _validationService = validationService;
            _ngonNguDomainService = ngonNguDomainService;
            _ngonNguRepository = ngonNguRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<NgonNgu>> GetAllNgonNgusAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu GetAllNgonNgusAsync");

            var ngonNgus = await _ngonNguRepository.GetAllNgonNgusAsync(cancellationToken);

            _logger.LogInformation("Orchestrator: Lấy danh sách NgonNgu thành công, tổng={Count}", ngonNgus.Count);

            return ngonNgus;
        }

        public async Task<NgonNgu> CreateNgonNguAsync(NgonNguCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateNgonNguAsync với tên ngôn ngữ {TenNN}", createDto.TenNN);

            await _validationService.ValidateAsync(createDto, cancellationToken);

            var ngonNgu = _ngonNguFactory.CreateNgonNgu(createDto);

            _ngonNguDomainService.EnsureCanCreate(ngonNgu);

            _logger.LogInformation("Orchestrator: NgonNgu {TenNN} đã được validate và khởi tạo thành công", createDto.TenNN);
            return ngonNgu;
        }

        public async Task<NgonNgu> UpdateNgonNguAsync(NgonNguUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateNgonNguAsync với Id={NgonNguId}", updateDto.NgonNguId);

            await _validationService.ValidateAsync(updateDto, cancellationToken);

            var ngonNgu = _ngonNguFactory.UpdateNgonNgu(updateDto);

            _ngonNguDomainService.EnsureCanUpdate(ngonNgu);

            _logger.LogInformation("Orchestrator: NgonNgu Id={NgonNguId} đã được cập nhật thành công", updateDto.NgonNguId);
            return ngonNgu;
        }

        public async Task DeleteNgonNguAsync(NgonNguDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteNgonNguAsync với Id={NgonNguId}", deleteDto.NgonNguId);

            await _validationService.ValidateAsync(deleteDto, cancellationToken);

            var ngonNgu = _ngonNguFactory.DeleteNgonNgu(deleteDto);

            _ngonNguDomainService.EnsureCanDelete(ngonNgu);

            _logger.LogInformation("Orchestrator: NgonNgu Id={NgonNguId} đã được xoá thành công", deleteDto.NgonNguId);
        }
    }
}
