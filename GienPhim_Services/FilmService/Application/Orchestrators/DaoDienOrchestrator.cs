using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Validators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class DaoDienOrchestrator : IDaoDienOrchestrator
    {
        private readonly IDaoDienFactory _daoDienFactory;
        private readonly IValidationService _validationService;
        private readonly IDaoDienDomainService _daoDienDomainService;
        private readonly ILogger<DaoDienOrchestrator> _logger;

        public DaoDienOrchestrator(
            IDaoDienFactory daoDienFactory,
            IValidationService validationService,
            IDaoDienDomainService daoDienDomainService,
            ILogger<DaoDienOrchestrator> logger)
        {
            _daoDienFactory = daoDienFactory;
            _validationService = validationService;
            _daoDienDomainService = daoDienDomainService;
            _logger = logger;
        }

        // Tạo đạo diễn mới
        public async Task<DaoDien> CreateDaoDienAsync(DaoDienCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateDaoDienAsync với tên đạo diễn {TenDD}", createDto.TenDD);

            // Validate bối cảnh: kiểm tra các điều kiện như trùng tên hoặc các lỗi input
            await _validationService.ValidateAsync(createDto, cancellationToken);

            // Tạo đối tượng DaoDien từ DTO
            var daoDienEntity = _daoDienFactory.CreateDaoDien(createDto);

            // Kiểm tra các điều kiện nghiệp vụ: Đảm bảo có thể tạo được đạo diễn mới
            _daoDienDomainService.EnsureCanCreate(daoDienEntity);

            _logger.LogInformation("Orchestrator: Đạo diễn {TenDD} đã được validate và khởi tạo thành công", createDto.TenDD);
            return daoDienEntity;  // Trả về Entity đã được tạo
        }

        // Cập nhật đạo diễn
        public async Task<DaoDien> UpdateDaoDienAsync(DaoDienUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateDaoDienAsync với Id={DaoDienId}", updateDto.DaoDienId);

            // Validate: kiểm tra tồn tại và các điều kiện cập nhật
            await _validationService.ValidateAsync(updateDto, cancellationToken);

            var daoDien = _daoDienFactory.UpdateDaoDien(updateDto);

            // Kiểm tra điều kiện nghiệp vụ
            _daoDienDomainService.EnsureCanUpdate(daoDien);

            _logger.LogInformation("Orchestrator: Đạo diễn Id={DaoDienId} đã được cập nhật thành công", updateDto.DaoDienId);
            return daoDien;
        }

        // Xoá đạo diễn
        public async Task DeleteDaoDienAsync(DaoDienDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteDaoDienAsync với Id={DaoDienId}", deleteDto.DaoDienId);

            // Validate: kiểm tra xem đạo diễn có tồn tại và có thể xoá hay không
            await _validationService.ValidateAsync(deleteDto, cancellationToken);

            // Kiểm tra điều kiện nghiệp vụ trước khi xóa
            var daoDien = _daoDienFactory.DeleteDaoDien(deleteDto);

            _daoDienDomainService.EnsureCanDelete(daoDien);

            _logger.LogInformation("Orchestrator: Đạo diễn Id={DaoDienId} đã được xoá thành công", deleteDto.DaoDienId);
        }
    }
}
