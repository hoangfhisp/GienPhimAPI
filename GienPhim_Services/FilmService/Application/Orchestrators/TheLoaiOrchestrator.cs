using GienPhim_Services.FilmService.Application.DTOs.Client.TheLoai;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class TheLoaiOrchestrator : ITheLoaiOrchestrator
    {
        private readonly ITheLoaiFactory _theLoaiFactory;
        private readonly ITheLoaiDomainService _theLoaiDomainService;
        private readonly ILogger<TheLoaiOrchestrator> _logger;

        public TheLoaiOrchestrator(
            ITheLoaiFactory theLoaiFactory,
            ITheLoaiDomainService theLoaiDomainService,
            ILogger<TheLoaiOrchestrator> logger)
        {
            _theLoaiFactory = theLoaiFactory;
            _theLoaiDomainService = theLoaiDomainService;
            _logger = logger;
        }

        public async Task<TheLoai> CreateTheLoaiAsync(TheLoaiCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateTheLoaiAsync với tên thể loại {TenTL}", createDto.TenTL);

            var theLoai = _theLoaiFactory.CreateTheLoai(createDto);

            _theLoaiDomainService.EnsureCanCreate(theLoai);

            _logger.LogInformation("Orchestrator: Thể loại {TenTL} đã được validate và khởi tạo thành công", createDto.TenTL);
            return theLoai;
        }

        public async Task<TheLoai> UpdateTheLoaiAsync(TheLoaiUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateTheLoaiAsync với Id={TheLoaiId}", updateDto.TheLoaiId);

            var theLoai = _theLoaiFactory.UpdateTheLoai(updateDto);

            _theLoaiDomainService.EnsureCanUpdate(theLoai);

            _logger.LogInformation("Orchestrator: Thể loại Id={TheLoaiId} đã được cập nhật thành công", updateDto.TheLoaiId);
            return theLoai;
        }

        public async Task DeleteTheLoaiAsync(TheLoaiDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteTheLoaiAsync với Id={TheLoaiId}", deleteDto.TheLoaiId);

            var theLoai = _theLoaiFactory.DeleteTheLoai(deleteDto);

            _theLoaiDomainService.EnsureCanDelete(theLoai);

            _logger.LogInformation("Orchestrator: Thể loại Id={TheLoaiId} đã được xoá thành công", deleteDto.TheLoaiId);
        }
    }
}
