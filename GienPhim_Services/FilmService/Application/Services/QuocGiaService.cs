using GienPhim_Services.FilmService.Application.DTOs.Client.QuocGia;
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
    public class QuocGiaService : IQuocGiaService
    {
        private readonly IQuocGiaOrchestrator _quocGiaOrchestrator;
        private readonly IQuocGiaResponseMapper _quocGiaResponseMapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<QuocGiaService> _logger;

        public QuocGiaService(
            IUnitOfWork unitOfWork,
            IQuocGiaOrchestrator quocGiaOrchestrator,
            IQuocGiaResponseMapper quocGiaResponseMapper,
            IPaginationMapper paginationMapper,
            ILogger<QuocGiaService> logger)
        {
            _unitOfWork = unitOfWork;
            _quocGiaOrchestrator = quocGiaOrchestrator;
            _quocGiaResponseMapper = quocGiaResponseMapper;
            _paginationMapper = paginationMapper;
            _logger = logger;
        }

        // Lấy danh sách quốc gia (có phân trang)
        public async Task<ResultListDTO<List<QuocGiaResponseDTO>>> GetAllQuocGiasAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetAllQuocGiasAsync với page={Page}, limit={Limit}", page, limit);

            var (quocGias, total, last) = await _unitOfWork.QuocGias.GetAllQuocGiasAsync(page, limit, cancellationToken);

            if (total == 0)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy quốc gia nào");
                throw new QuocGiaNotFoundException();
            }

            var resultDtos = _quocGiaResponseMapper.ToDtoList(quocGias);

            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            _logger.LogInformation("ApplicationService: Lấy danh sách QuốcGia thành công, tổng={Total}", total);

            return ResultListDTO<List<QuocGiaResponseDTO>>.Success(resultDtos, "Lấy danh sách quốc gia thành công", pagination);
        }

        // Xem chi tiết quốc gia
        public async Task<ResultDTO<QuocGiaResponseDTO>> GetDetailAsync(int quocGiaId, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetDetailAsync cho QuocGiaId={QuocGiaId}", quocGiaId);

            var entity = await _unitOfWork.QuocGias.GetByIdAsync(quocGiaId, ct);
            if (entity is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy Quốc Gia Id={QuocGiaId}", quocGiaId);
                throw new QuocGiaNotFoundException(quocGiaId.ToString());
            }

            var dto = _quocGiaResponseMapper.ToDto(entity);

            _logger.LogInformation("ApplicationService: Lấy chi tiết Quốc Gia Id={QuocGiaId} thành công", quocGiaId);

            return ResultDTO<QuocGiaResponseDTO>.Success(dto, "Lấy chi tiết quốc gia thành công");
        }

        // Tạo quốc gia
        public async Task<ResultDTO> CreateQuocGiaAsync(QuocGiaCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu CreateQuocGiaAsync với tên {TenQG}", createDto.TenQG);

            var quocGia = await _quocGiaOrchestrator.CreateQuocGiaAsync(createDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.QuocGias.CreateAsync(quocGia, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Tạo QuocGia {TenQG} thất bại!", quocGia.TenQG);
                await transaction.RollbackAsync(cancellationToken);
                throw new CreateQuocGiaFailedException(quocGia.TenQG);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Tạo QuocGia {TenQG} thành công!", quocGia.TenQG);
            return ResultDTO.Success($"Tạo quốc gia {quocGia.TenQG} thành công");
        }

        // Cập nhật quốc gia
        public async Task<ResultDTO<UpdateQuocGiaResponseDTO>> UpdateQuocGiaAsync(QuocGiaUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu UpdateQuocGiaAsync với Id={QuocGiaId}", updateDto.QuocGiaId);

            var quocGia = await _quocGiaOrchestrator.UpdateQuocGiaAsync(updateDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.QuocGias.UpdateAsync(quocGia, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Cập nhật QuocGia {QuocGiaId} thất bại!", updateDto.QuocGiaId);
                await transaction.RollbackAsync(cancellationToken);
                throw new UpdateQuocGiaFailedException(quocGia.TenQG);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Cập nhật QuocGia {QuocGiaId} thành công!", quocGia.QuocGiaId);

            var response = _quocGiaResponseMapper.ToUpdateDto(quocGia);

            return ResultDTO<UpdateQuocGiaResponseDTO>.Success(
                response,
                $"Cập nhật quốc gia {quocGia.TenQG} thành công"
            );
        }

        // Xóa quốc gia
        public async Task<ResultDTO> DeleteQuocGiaAsync(QuocGiaDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu DeleteQuocGiaAsync với Id={QuocGiaId}", deleteDto.QuocGiaId);

            await _quocGiaOrchestrator.DeleteQuocGiaAsync(deleteDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var quocGia = await _unitOfWork.QuocGias.GetByIdAsync(deleteDto.QuocGiaId, cancellationToken);
            if (quocGia is null)
            {
                _logger.LogWarning("ApplicationService: QuocGia không tồn tại khi xóa {QuocGiaId}", deleteDto.QuocGiaId);
                return ResultDTO.Fail($"Không tìm thấy quốc gia với Id {deleteDto.QuocGiaId}");
            }

            await _unitOfWork.QuocGias.DeleteAsync(quocGia, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Xóa QuocGia {QuocGiaId} thành công!", deleteDto.QuocGiaId);
            return ResultDTO.Success($"Xóa quốc gia {quocGia.TenQG} thành công");
        }

        // Tìm kiếm quốc gia theo từ khóa (gần đúng)
        public async Task<ResultListDTO<List<QuocGiaResponseDTO>>> SearchQuocGiasAsync(string keyword, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu SearchQuocGiasAsync với keyword = {Keyword}", keyword);

            // Gọi repository
            var quocGias = await _unitOfWork.QuocGias.SearchQuocGiasAsync(keyword, ct);

            if (quocGias == null || !quocGias.Any())
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy quốc gia nào phù hợp với keyword = {Keyword}", keyword);
                throw new QuocGiaNotFoundException();
            }

            // Map sang DTO
            var resultDtos = _quocGiaResponseMapper.ToDtoList(quocGias);

            _logger.LogInformation("ApplicationService: Tìm kiếm quốc gia thành công, tổng={Count}", resultDtos.Count);

            // Trả kết quả
            return ResultListDTO<List<QuocGiaResponseDTO>>.Success(resultDtos, "Tìm kiếm quốc gia thành công");
        }
    }
}
