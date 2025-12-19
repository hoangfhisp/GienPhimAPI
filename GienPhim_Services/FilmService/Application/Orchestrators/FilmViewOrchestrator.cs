using GienPhim_Services.FilmService.Application.DTOs.Client.FilmView;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class FilmViewOrchestrator : IFilmViewOrchestrator
    {
        private readonly IFilmViewFactory _factory;
        private readonly IFilmViewDomainService _domainService;
        private readonly ILogger<FilmViewOrchestrator> _logger;

        public FilmViewOrchestrator(
            IFilmViewFactory factory,
            IFilmViewDomainService domainService,
            ILogger<FilmViewOrchestrator> logger)
        {
            _factory = factory;
            _domainService = domainService;
            _logger = logger;
        }

        public async Task<FilmView> CreateAsync(FilmViewCreateDTO dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu Create FilmView cho PhimId={PhimId}, UserId={UserId}", dto.PhimId, dto.UserId);

            var filmView = _factory.Create(dto);

            _domainService.EnsureCanCreate(filmView);

            _logger.LogInformation("Orchestrator: FilmView đã validate thành công cho PhimId={PhimId}, UserId={UserId}", dto.PhimId, dto.UserId);

            return filmView;
        }

        public async Task<FilmView> UpdateAsync(FilmViewUpdateDTO dto, FilmView? existing, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu Update FilmView Id={FilmViewId}", dto.FilmViewId);

            var filmView = _factory.Update(dto);

            _domainService.EnsureCanUpdate(filmView, existing);

            _logger.LogInformation("Orchestrator: FilmView Id={FilmViewId} đã validate thành công", dto.FilmViewId);

            return filmView;
        }

        public async Task DeleteAsync(FilmViewDeleteDTO dto, FilmView? existing, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu Delete FilmView Id={FilmViewId}", dto.FilmViewId);

            var filmView = new FilmView { FilmViewId = dto.FilmViewId };

            _domainService.EnsureCanDelete(existing);

            _logger.LogInformation("Orchestrator: FilmView Id={FilmViewId} đã validate để xoá", dto.FilmViewId);
        }
    }
}
