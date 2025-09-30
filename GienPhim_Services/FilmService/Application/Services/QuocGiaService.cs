using GienPhim_Services.FilmService.Application.DTOs.Client.QuocGia;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Services;

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
    }
}
