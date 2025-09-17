using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;
using GienPhim_Services.FilmService.Shared.Kernel;

namespace GienPhim_Services.FilmService.Domain.Services
{
    public class FilmDomainService : IFilmDomainService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FilmDomainService> _logger;

        public FilmDomainService(IUnitOfWork unitOfWork, ILogger<FilmDomainService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // Lấy danh sách tất cả đạo diễn 
        public async Task<List<DaoDien>> GetAllDaoDiensAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Domain Service: Bắt đầu GetAllDaoDiensAsync , trang: {Page}, giới hạn: {Limit}", page, limit);
            var daoDiens = await _unitOfWork.DaoDiens.GetAllDaoDiensAsync(cancellationToken);
            if (daoDiens == null || !daoDiens.Any()) // any để kiểm tra có dữ liệu hay không true/false
            {
                _logger.LogWarning("Domain Service: Không tìm thấy đạo diễn nào trong hệ thống.");
                throw new DaoDienNotFoundException();
            }

            var data = daoDiens
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToList();

            _logger.LogInformation("Domain Service: Lấy danh sách đạo diễn thành công, tổng: {Total}", daoDiens.Count());
            return data;
        }
    }
}
