using GienPhim_Services.FilmService.Application.DTOs.Client.BotConfig;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class BotConfigService : IBotConfigService
    {
        private readonly IBotConfigOrchestrator _orchestrator;
        private readonly ILogger<BotConfigService> _logger;

        public BotConfigService(IBotConfigOrchestrator orchestrator, ILogger<BotConfigService> logger)
        {
            _orchestrator = orchestrator;
            _logger = logger;
        }

        // Lấy cấu hình Bot
        public async Task<ResultDTO<UpdateBotConfigResponseDTO>> GetBotConfigAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Lấy cấu hình bot hiện tại...");

            try
            {
                var result = await _orchestrator.GetAsync(cancellationToken);
                return ResultDTO<UpdateBotConfigResponseDTO>.Success(result, "Lấy cấu hình bot thành công.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ApplicationService: Lấy cấu hình bot thất bại.");
                throw new UpdateBotConfigFailedException("Lấy cấu hình", ex.Message);
            }
        }

        // Cập nhật cấu hình Bot
        public async Task<ResultDTO<UpdateBotConfigResponseDTO>> UpdateBotConfigAsync(BotConfigUpdateDTO dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Cập nhật cấu hình bot...");

            try
            {
                var updated = await _orchestrator.UpdateAsync(dto, cancellationToken);
                return ResultDTO<UpdateBotConfigResponseDTO>.Success(updated, "Cập nhật cấu hình bot thành công.");
            }
            catch (BotConfigNotFoundException ex)
            {
                _logger.LogWarning(ex, "ApplicationService: Không tìm thấy cấu hình bot.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ApplicationService: Cập nhật cấu hình bot thất bại.");
                throw new UpdateBotConfigFailedException("Cập nhật cấu hình", ex.Message);
            }
        }
    }
}
