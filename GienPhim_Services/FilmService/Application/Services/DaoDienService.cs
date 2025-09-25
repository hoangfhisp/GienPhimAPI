using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Services;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class DaoDienService : IDaoDienService
    {
        private readonly IDaoDienOrchestrator _daoDienOrchestrator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DaoDienService> _logger;

        public DaoDienService(
            IUnitOfWork unitOfWork,
            IDaoDienOrchestrator daoDienOrchestrator,
            ILogger<DaoDienService> logger)
        {
            _unitOfWork = unitOfWork;
            _daoDienOrchestrator = daoDienOrchestrator;
            _logger = logger;
        }

        // Tạo DaoDien mới
        public async Task<ResultDTO> CreateDaoDienAsync(DaoDienCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu CreateDaoDienAsync với tên đạo diễn {TenDD}", createDto.TenDD);

            // Gọi Orchestrator để tạo DaoDien
            var daoDien = await _daoDienOrchestrator.CreateDaoDienAsync(createDto, cancellationToken);

            // Bắt đầu transaction
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Lưu DaoDien vào Repository
            var result = await _unitOfWork.DaoDiens.CreateAsync(daoDien, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Tạo DaoDien {TenDD} thất bại!", daoDien.TenDD);
                await transaction.RollbackAsync(cancellationToken);
                throw new CreateDaoDienFailedException(daoDien.TenDD);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Tạo DaoDien {TenDD} thành công!", daoDien.TenDD);
            return ResultDTO.Success($"Tạo đạo diễn {daoDien.TenDD} thành công");
        }

        // Cập nhật DaoDien
        public async Task<ResultDTO> UpdateDaoDienAsync(DaoDienUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu UpdateDaoDienAsync với Id={DaoDienId}", updateDto.DaoDienId);

            // Gọi Orchestrator để cập nhật DaoDien
            var daoDien = await _daoDienOrchestrator.UpdateDaoDienAsync(updateDto, cancellationToken);

            // Bắt đầu transaction
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // Cập nhật DaoDien vào Repository
            var result = await _unitOfWork.DaoDiens.UpdateAsync(daoDien, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Cập nhật DaoDien {DaoDienId} thất bại!", updateDto.DaoDienId);
                await transaction.RollbackAsync(cancellationToken);
                throw new UpdateDaoDienFailedException(daoDien.TenDD);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Cập nhật DaoDien {DaoDienId} thành công!", daoDien.DaoDienId);

            return ResultDTO.Success($"Cập nhật đạo diễn {daoDien.TenDD} thành công");
        }

        // Xoá DaoDien
        public async Task<ResultDTO> DeleteDaoDienAsync(DaoDienDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu DeleteDaoDienAsync với Id={DaoDienId}", deleteDto.DaoDienId);

            await _daoDienOrchestrator.DeleteDaoDienAsync(deleteDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var daoDien = await _unitOfWork.DaoDiens.GetByIdAsync(deleteDto.DaoDienId, cancellationToken);
            if (daoDien is null)
            {
                _logger.LogWarning("ApplicationService: DaoDien không tồn tại khi xóa {DaoDienId}", deleteDto.DaoDienId);
                return ResultDTO.Fail($"Không tìm thấy đạo diễn với Id {deleteDto.DaoDienId}");
            }

            await _unitOfWork.DaoDiens.DeleteAsync(daoDien, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Xóa DaoDien {DaoDienId} thành công!", deleteDto.DaoDienId);
            return ResultDTO.Success($"Xóa đạo diễn {daoDien.TenDD} thành công");
        }
    }
}
