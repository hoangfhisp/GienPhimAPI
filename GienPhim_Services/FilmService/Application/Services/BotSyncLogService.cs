using GienPhim_Services.FilmService.Application.DTOs.Client.BotSyncLogs;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class BotSyncLogService : IBotSyncLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BotSyncLogService> _logger;
        private readonly IPaginationMapper _paginationMapper; // bạn đang dùng cái này ở phim

        public BotSyncLogService(
            IUnitOfWork unitOfWork,
            ILogger<BotSyncLogService> logger,
            IPaginationMapper paginationMapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _paginationMapper = paginationMapper;
        }

        public async Task CreateAsync(BotSyncLogCreateDTO model, CancellationToken cancellationToken = default)
        {
            var log = new BotSyncLog
            {
                PhimName = model.PhimName?.Trim() ?? string.Empty,
                PhimSlug = model.PhimSlug?.Trim() ?? string.Empty,
                Action = model.Action,
                SyncedAt = model.SyncedAt ?? DateTime.UtcNow,
                Message = string.IsNullOrWhiteSpace(model.Message) ? null : model.Message.Trim()
            };

            await _unitOfWork.BotSyncLogs.AddAsync(log, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("BotSyncLog created: {Slug} - {Action}", log.PhimSlug, log.Action);
        }

        public async Task<ResultListDTO<List<BotSyncLogResponseDTO>>> GetAllAsync(
            int page, int limit, CancellationToken cancellationToken = default)
        {
            var (logs, total, last) = await _unitOfWork.BotSyncLogs.GetAllAsync(page, limit, cancellationToken);

            // Với log: rỗng là bình thường => trả list rỗng (đừng throw)
            var dtos = logs.Select(x => new BotSyncLogResponseDTO
            {
                BotSyncLogId = x.BotSyncLogId,
                PhimName = x.PhimName,
                PhimSlug = x.PhimSlug,
                Action = x.Action,
                SyncedAt = x.SyncedAt,
                Message = x.Message
            }).ToList();

            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            return ResultListDTO<List<BotSyncLogResponseDTO>>.Success(
                dtos,
                "Lấy lịch sử bot thành công",
                pagination);
        }
    }
}
