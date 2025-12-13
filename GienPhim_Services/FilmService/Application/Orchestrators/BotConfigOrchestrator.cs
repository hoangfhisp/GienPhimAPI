using GienPhim_Services.FilmService.Application.DTOs.Client.BotConfig;
using GienPhim_Services.FilmService.Application.Exceptions.Invalids;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class BotConfigOrchestrator : IBotConfigOrchestrator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBotConfigFactory _factory;
        private readonly IBotConfigMapper _mapper;
        private readonly ILogger<BotConfigOrchestrator> _logger;

        public BotConfigOrchestrator(
            IUnitOfWork unitOfWork,
            IBotConfigFactory factory,
            IBotConfigMapper mapper,
            ILogger<BotConfigOrchestrator> logger)
        {
            _unitOfWork = unitOfWork;
            _factory = factory;
            _mapper = mapper;
            _logger = logger;
        }

        // Lấy cấu hình bot hiện tại
        public async Task<UpdateBotConfigResponseDTO> GetAsync(CancellationToken ct = default)
        {
            _logger.LogInformation("Orchestrator: Đang lấy cấu hình Bot...");

            var config = await _unitOfWork.BotConfigs.GetAsync(ct);
            if (config == null)
            {
                _logger.LogWarning("Orchestrator: Không tìm thấy cấu hình Bot, tạo mặc định...");
                config = _factory.CreateDefault();
                await _unitOfWork.BotConfigs.CreateAsync(config, ct);
            }

            return _mapper.ToResponseDto(config);
        }

        // Cập nhật cấu hình bot
        public async Task<UpdateBotConfigResponseDTO> UpdateAsync(BotConfigUpdateDTO dto, CancellationToken ct = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu cập nhật cấu hình Bot...");

            var existing = await _unitOfWork.BotConfigs.GetAsync(ct);
            if (existing == null)
                throw new BotConfigNotFoundException();

            try
            {
                _factory.Update(existing, dto);
                await _unitOfWork.BotConfigs.UpdateAsync(existing, ct);
                _logger.LogInformation("Orchestrator: Cập nhật cấu hình Bot thành công.");
                return _mapper.ToResponseDto(existing);
            }
            catch (InvalidBotConfigException ex)
            {
                _logger.LogWarning(ex, "Orchestrator: Dữ liệu cấu hình không hợp lệ.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Orchestrator: Cập nhật cấu hình Bot thất bại.");
                throw;
            }
        }
    }
}
