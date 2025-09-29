using GienPhim_Services.FilmService.Application.DTOs.Client.DienVien;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class DienVienOrchestrator : IDienVienOrchestrator
    {
        private readonly IDienVienFactory _dienVienfactory;
        private readonly IDienVienDomainService _domainService;
        private readonly ILogger<DienVienOrchestrator> _logger;

        public DienVienOrchestrator(
            IDienVienFactory dienVienfactory,
            IDienVienDomainService domainService,
            ILogger<DienVienOrchestrator> logger)
        {
            _dienVienfactory = dienVienfactory;
            _domainService = domainService;
            _logger = logger;
        }

        public async Task<DienVien> CreateDienVienAsync(DienVienCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateDienVienAsync với tên {TenDV}", createDto.TenDV);

            var entity = _dienVienfactory.CreateDienVien(createDto);
            _domainService.EnsureCanCreate(entity);

            _logger.LogInformation("Orchestrator: Diễn viên {TenDV} đã được validate và khởi tạo thành công", createDto.TenDV);
            return entity;
        }

        public async Task<DienVien> UpdateDienVienAsync(DienVienUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateDienVienAsync với Id={DienVienId}", updateDto.DienVienId);

            var entity = _dienVienfactory.UpdateDienVien(updateDto);
            _domainService.EnsureCanUpdate(entity);

            _logger.LogInformation("Orchestrator: Diễn viên Id={DienVienId} đã được cập nhật thành công", updateDto.DienVienId);
            return entity;
        }

        public async Task DeleteDienVienAsync(DienVienDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteDienVienAsync với Id={DienVienId}", deleteDto.DienVienId);

            var entity = _dienVienfactory.DeleteDienVien(deleteDto);
            _domainService.EnsureCanDelete(entity);

            _logger.LogInformation("Orchestrator: Diễn viên Id={DienVienId} đã được xoá thành công", deleteDto.DienVienId);
        }
    }
}
