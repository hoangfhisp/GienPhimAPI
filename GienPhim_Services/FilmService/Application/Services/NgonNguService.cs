using GienPhim_Services.FilmService.Application.DTOs.Client.NgonNgu;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class NgonNguService : INgonNguService
    {
        private readonly INgonNguOrchestrator _ngonNguOrchestrator;
        private readonly INgonNguResponseMapper _ngonNguResponseMapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<NgonNguService> _logger;

        public NgonNguService(
            IUnitOfWork unitOfWork,
            INgonNguOrchestrator ngonNguOrchestrator,
            INgonNguResponseMapper ngonNguResponseMapper,
            IPaginationMapper paginationMapper,
            ILogger<NgonNguService> logger)
        {
            _unitOfWork = unitOfWork;
            _ngonNguOrchestrator = ngonNguOrchestrator;
            _ngonNguResponseMapper = ngonNguResponseMapper;
            _paginationMapper = paginationMapper;
            _logger = logger;
        }

        // Lấy danh sách ngôn ngữ (có phân trang)
        public async Task<ResultListDTO<List<NgonNguResponseDTO>>> GetAllNgonNgusAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetAllNgonNgusAsync với page={Page}, limit={Limit}", page, limit);

            var (ngonNgus, total, last) = await _unitOfWork.NgonNgus.GetAllNgonNgusAsync(page, limit, cancellationToken);

            if (total == 0)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy ngôn ngữ nào");
                throw new NgonNguNotFoundException();
            }

            var resultDtos = _ngonNguResponseMapper.ToDtoList(ngonNgus);

            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            _logger.LogInformation("ApplicationService: Lấy danh sách NgonNgu thành công, tổng={Total}", total);

            return ResultListDTO<List<NgonNguResponseDTO>>.Success(resultDtos, "Lấy danh sách ngôn ngữ thành công", pagination);
        }

        // Xem chi tiết ngôn ngữ
        public async Task<ResultDTO<NgonNguResponseDTO>> GetDetailAsync(int ngonNguId, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetDetailAsync cho NgonNguId={NgonNguId}", ngonNguId);

            var ngonNgu = await _unitOfWork.NgonNgus.GetByIdAsync(ngonNguId, ct);
            if (ngonNgu is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy Ngôn Ngữ Id={NgonNguId}", ngonNguId);
                throw new NgonNguNotFoundException(ngonNguId.ToString());
            }

            var dto = _ngonNguResponseMapper.ToDto(ngonNgu);

            _logger.LogInformation("ApplicationService: Lấy chi tiết Ngôn Ngữ Id={NgonNguId} thành công", ngonNguId);

            return ResultDTO<NgonNguResponseDTO>.Success(dto, "Lấy chi tiết ngôn ngữ thành công");
        }

        // Tạo ngôn ngữ mới
        public async Task<ResultDTO> CreateNgonNguAsync(NgonNguCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu CreateNgonNguAsync với tên {TenNN}", createDto.TenNN);

            var ngonNgu = await _ngonNguOrchestrator.CreateNgonNguAsync(createDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.NgonNgus.CreateAsync(ngonNgu, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Tạo NgonNgu {TenNN} thất bại!", ngonNgu.TenNN);
                await transaction.RollbackAsync(cancellationToken);
                throw new CreateNgonNguFailedException(ngonNgu.TenNN);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Tạo NgonNgu {TenNN} thành công!", ngonNgu.TenNN);
            return ResultDTO.Success($"Tạo ngôn ngữ {ngonNgu.TenNN} thành công");
        }

        // Cập nhật ngôn ngữ
        public async Task<ResultDTO<UpdateNgonNguResponseDTO>> UpdateNgonNguAsync(NgonNguUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu UpdateNgonNguAsync với Id={NgonNguId}", updateDto.NgonNguId);

            var ngonNgu = await _ngonNguOrchestrator.UpdateNgonNguAsync(updateDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.NgonNgus.UpdateAsync(ngonNgu, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Cập nhật NgonNgu {NgonNguId} thất bại!", updateDto.NgonNguId);
                await transaction.RollbackAsync(cancellationToken);
                throw new UpdateNgonNguFailedException(ngonNgu.TenNN);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Cập nhật NgonNgu {NgonNguId} thành công!", ngonNgu.NgonNguId);

            var response = _ngonNguResponseMapper.ToUpdateDto(ngonNgu);

            return ResultDTO<UpdateNgonNguResponseDTO>.Success(
                response,
                $"Cập nhật ngôn ngữ {ngonNgu.TenNN} thành công");
        }

        // Xóa ngôn ngữ
        public async Task<ResultDTO> DeleteNgonNguAsync(NgonNguDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu DeleteNgonNguAsync với Id={NgonNguId}", deleteDto.NgonNguId);

            await _ngonNguOrchestrator.DeleteNgonNguAsync(deleteDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var ngonNgu = await _unitOfWork.NgonNgus.GetByIdAsync(deleteDto.NgonNguId, cancellationToken);
            if (ngonNgu is null)
            {
                _logger.LogWarning("ApplicationService: NgonNgu không tồn tại khi xóa {NgonNguId}", deleteDto.NgonNguId);
                return ResultDTO.Fail($"Không tìm thấy ngôn ngữ với Id {deleteDto.NgonNguId}");
            }

            await _unitOfWork.NgonNgus.DeleteAsync(ngonNgu, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Xóa NgonNgu {NgonNguId} thành công!", deleteDto.NgonNguId);
            return ResultDTO.Success($"Xóa ngôn ngữ {ngonNgu.TenNN} thành công");
        }

        // Tìm kiếm ngôn ngữ theo từ khóa (gần đúng)
        public async Task<ResultListDTO<List<NgonNguResponseDTO>>> SearchNgonNgusAsync(string keyword, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu SearchNgonNgusAsync với keyword = {Keyword}", keyword);

            // Gọi repository
            var ngonNgus = await _unitOfWork.NgonNgus.SearchNgonNgusAsync(keyword, ct);

            if (ngonNgus == null || !ngonNgus.Any())
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy ngôn ngữ nào phù hợp với keyword = {Keyword}", keyword);
                throw new NgonNguNotFoundException();
            }

            // Map sang DTO
            var resultDtos = _ngonNguResponseMapper.ToDtoList(ngonNgus);

            _logger.LogInformation("ApplicationService: Tìm kiếm ngôn ngữ thành công, tổng={Count}", resultDtos.Count);

            return ResultListDTO<List<NgonNguResponseDTO>>.Success(resultDtos, "Tìm kiếm ngôn ngữ thành công");
        }
    }
}
