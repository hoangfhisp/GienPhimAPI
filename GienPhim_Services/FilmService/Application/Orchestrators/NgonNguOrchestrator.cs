using GienPhim_Services.FilmService.Application.DTOs.Client.NgonNgu;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class NgonNguOrchestrator : INgonNguOrchestrator
    {
        private readonly INgonNguFactory _ngonNguFactory;
        private readonly INgonNguDomainService _ngonNguDomainService;
        private readonly ILogger<NgonNguOrchestrator> _logger;

        public NgonNguOrchestrator(
            INgonNguFactory ngonNguFactory,
            INgonNguDomainService ngonNguDomainService,
            ILogger<NgonNguOrchestrator> logger)
        {
            _ngonNguFactory = ngonNguFactory;
            _ngonNguDomainService = ngonNguDomainService;
            _logger = logger;
        }

        public async Task<NgonNgu> CreateNgonNguAsync(NgonNguCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateNgonNguAsync với tên ngôn ngữ {TenNN}", createDto.TenNN);

            var ngonNgu = _ngonNguFactory.CreateNgonNgu(createDto);

            _ngonNguDomainService.EnsureCanCreate(ngonNgu);

            _logger.LogInformation("Orchestrator: NgonNgu {TenNN} đã được validate và khởi tạo thành công", createDto.TenNN);
            return ngonNgu;
        }

        public async Task<NgonNgu> UpdateNgonNguAsync(NgonNguUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateNgonNguAsync với Id={NgonNguId}", updateDto.NgonNguId);

            var ngonNgu = _ngonNguFactory.UpdateNgonNgu(updateDto);

            _ngonNguDomainService.EnsureCanUpdate(ngonNgu);

            _logger.LogInformation("Orchestrator: NgonNgu Id={NgonNguId} đã được cập nhật thành công", updateDto.NgonNguId);
            return ngonNgu;
        }

        public async Task DeleteNgonNguAsync(NgonNguDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteNgonNguAsync với Id={NgonNguId}", deleteDto.NgonNguId);

            var ngonNgu = _ngonNguFactory.DeleteNgonNgu(deleteDto);

            _ngonNguDomainService.EnsureCanDelete(ngonNgu);

            _logger.LogInformation("Orchestrator: NgonNgu Id={NgonNguId} đã được xoá thành công", deleteDto.NgonNguId);
        }
    }
}
