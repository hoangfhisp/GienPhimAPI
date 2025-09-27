using GienPhim_Services.FilmService.Application.DTOs.Client.DienVien;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Validators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class DienVienOrchestrator : IDienVienOrchestrator
    {
        private readonly IDienVienFactory _factory;
        private readonly IValidationService _validationService;
        private readonly IDienVienDomainService _domainService;
        private readonly IDienVienRepository _repository;
        private readonly ILogger<DienVienOrchestrator> _logger;

        public DienVienOrchestrator(
            IDienVienFactory factory,
            IValidationService validationService,
            IDienVienDomainService domainService,
            IDienVienRepository repository,
            ILogger<DienVienOrchestrator> logger)
        {
            _factory = factory;
            _validationService = validationService;
            _domainService = domainService;
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<DienVien>> GetAllDienViensAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu GetAllDienViensAsync");
            var dienViens = await _repository.GetAllDienViensAsync(cancellationToken);
            _logger.LogInformation("Orchestrator: Lấy danh sách diễn viên thành công, tổng={Count}", dienViens.Count);
            return dienViens;
        }

        public async Task<DienVien> CreateDienVienAsync(DienVienCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateDienVienAsync với tên {TenDV}", createDto.TenDV);

            await _validationService.ValidateAsync(createDto, cancellationToken);

            var entity = _factory.CreateDienVien(createDto);
            _domainService.EnsureCanCreate(entity);

            _logger.LogInformation("Orchestrator: Diễn viên {TenDV} đã được validate và khởi tạo thành công", createDto.TenDV);
            return entity;
        }

        public async Task<DienVien> UpdateDienVienAsync(DienVienUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateDienVienAsync với Id={DienVienId}", updateDto.DienVienId);

            await _validationService.ValidateAsync(updateDto, cancellationToken);

            var entity = _factory.UpdateDienVien(updateDto);
            _domainService.EnsureCanUpdate(entity);

            _logger.LogInformation("Orchestrator: Diễn viên Id={DienVienId} đã được cập nhật thành công", updateDto.DienVienId);
            return entity;
        }

        public async Task DeleteDienVienAsync(DienVienDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteDienVienAsync với Id={DienVienId}", deleteDto.DienVienId);

            await _validationService.ValidateAsync(deleteDto, cancellationToken);

            var entity = _factory.DeleteDienVien(deleteDto);
            _domainService.EnsureCanDelete(entity);

            _logger.LogInformation("Orchestrator: Diễn viên Id={DienVienId} đã được xoá thành công", deleteDto.DienVienId);
        }
    }
}
