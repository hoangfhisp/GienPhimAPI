using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class FilmOrchestrator : IFilmOrchestrator
    {
        private readonly IFilmDomainService _filmDomainService;
        private readonly IPaginationMapper _paginationMapper;
        private readonly IDaoDienResponseMapper _daoDienResponseMapper;
        private readonly ILogger<FilmOrchestrator> _logger;

        public FilmOrchestrator(IFilmDomainService filmDomainService, IPaginationMapper paginationMapper
            , IDaoDienResponseMapper daoDienResponseMapper, ILogger<FilmOrchestrator> logger)
        {
            _filmDomainService = filmDomainService;
            _daoDienResponseMapper = daoDienResponseMapper;
            _paginationMapper = paginationMapper;
            _logger = logger;
        }

        // lấy danh sách tất cả đạo diễn 
        public async Task<ResultListDTO<List<DaoDienResponseDTO>>> GetAllDaoDiensAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu GetAllDaoDiensAsync , trang: {Page}, giới hạn: {Limit}", page, limit);
            var daoDiens = await _filmDomainService.GetAllDaoDiensAsync(page, limit, cancellationToken);

            var total = daoDiens.Count();
            var last = (int)Math.Ceiling((double)total / limit);
            
            var pagination = _paginationMapper.ToDto(page, limit, last,total);

            var ResultDaoDien = _daoDienResponseMapper.toDtoList(daoDiens);

            _logger.LogInformation("Orchestrator: Lấy danh sách đạo diễn thành công, trang: {Page}, giới hạn: {Limit}, tổng: {Total}, trang cuối: {Last}", page, limit, total, last);
            return ResultListDTO<List<DaoDienResponseDTO>>.Success(ResultDaoDien, "Lấy danh sách đạo diễn thành công",pagination);
        }
    }
}
