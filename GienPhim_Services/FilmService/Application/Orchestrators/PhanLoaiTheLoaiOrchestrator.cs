using GienPhim_Services.FilmService.Application.DTOs.Client.PhanLoaiTheLoai;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class PhanLoaiTheLoaiOrchestrator : IPhanLoaiTheLoaiOrchestrator
    {
        private readonly IPhanLoaiTheLoaiFactory _factory;
        private readonly IPhanLoaiTheLoaiDomainService _domainService;
        private readonly ILogger<PhanLoaiTheLoaiOrchestrator> _logger;

        public PhanLoaiTheLoaiOrchestrator(
            IPhanLoaiTheLoaiFactory factory,
            IPhanLoaiTheLoaiDomainService domainService,
            ILogger<PhanLoaiTheLoaiOrchestrator> logger)
        {
            _factory = factory;
            _domainService = domainService;
            _logger = logger;
        }

        public async Task<PhanLoaiTheLoai> CreateAsync(PhanLoaiTheLoaiCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu Create PhanLoaiTheLoai với PhimId={PhimId}, TheLoaiId={TheLoaiId}", createDto.PhimId, createDto.TheLoaiId);

            var phanLoaiTheLoai = _factory.Create(createDto);

            // domain service validation
            _domainService.EnsureCanCreate(phanLoaiTheLoai, new List<PhanLoaiTheLoai>());

            _logger.LogInformation("Orchestrator: PhanLoaiTheLoai với PhimId={PhimId}, TheLoaiId={TheLoaiId} đã validate thành công", createDto.PhimId, createDto.TheLoaiId);

            return phanLoaiTheLoai;
        }

        public async Task<PhanLoaiTheLoai> UpdateAsync(PhanLoaiTheLoaiUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu Update PhanLoaiTheLoai với PhimId={PhimId}, TheLoaiId={TheLoaiId}", updateDto.PhimId, updateDto.TheLoaiId);

            var phanLoaiTheLoai = _factory.Update(updateDto);

            // domain service validation
            _domainService.EnsureCanUpdate(phanLoaiTheLoai, phanLoaiTheLoai);

            _logger.LogInformation("Orchestrator: Update thành công cho PhanLoaiTheLoai với PhimId={PhimId}, TheLoaiId={TheLoaiId}", updateDto.PhimId, updateDto.TheLoaiId);

            return phanLoaiTheLoai;
        }

        public async Task DeleteAsync(PhanLoaiTheLoaiDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu Delete PhanLoaiTheLoai với PhimId={PhimId}, TheLoaiId={TheLoaiId}", deleteDto.PhimId, deleteDto.TheLoaiId);

            var phanLoaiTheLoai = _factory.Delete(deleteDto);

            _domainService.EnsureCanDelete(phanLoaiTheLoai);

            _logger.LogInformation("Orchestrator: Validate xóa thành công cho PhanLoaiTheLoai với PhimId={PhimId}, TheLoaiId={TheLoaiId}", deleteDto.PhimId, deleteDto.TheLoaiId);
        }
    }
}
