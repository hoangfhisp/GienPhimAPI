using GienPhim_Services.FilmService.Application.DTOs.Client.TheLoai;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Services;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class TheLoaiService : ITheLoaiService
    {
        private readonly ITheLoaiOrchestrator _theLoaiOrchestrator;
        private readonly ITheLoaiResponseMapper _theLoaiResponseMapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TheLoaiService> _logger;

        public TheLoaiService(
            IUnitOfWork unitOfWork,
            ITheLoaiOrchestrator theLoaiOrchestrator,
            ITheLoaiResponseMapper theLoaiResponseMapper,
            IPaginationMapper paginationMapper,
            ILogger<TheLoaiService> logger)
        {
            _unitOfWork = unitOfWork;
            _theLoaiOrchestrator = theLoaiOrchestrator;
            _theLoaiResponseMapper = theLoaiResponseMapper;
            _paginationMapper = paginationMapper;
            _logger = logger;
        }

        public async Task<ResultListDTO<List<TheLoaiResponseDTO>>> GetAllTheLoaisAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetAllTheLoaisAsync với page={Page}, limit={Limit}", page, limit);

            var (theLoais, total, last) = await _unitOfWork.TheLoais.GetAllTheLoaisAsync(page, limit, cancellationToken);

            if (total == 0)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy thể loại nào");
                throw new TheLoaiNotFoundException();
            }

            var resultDtos = _theLoaiResponseMapper.toDtoList(theLoais);

            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            _logger.LogInformation("ApplicationService: Lấy danh sách Thể Loại thành công, tổng={Total}", total);

            return ResultListDTO<List<TheLoaiResponseDTO>>.Success(
                resultDtos,
                "Lấy danh sách thể loại thành công",
                pagination
            );
        }

        public async Task<ResultDTO<TheLoaiResponseDTO>> GetDetailAsync(int theLoaiId, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetDetailAsync cho TheLoaiId={TheLoaiId}", theLoaiId);

            var entity = await _unitOfWork.TheLoais.GetByIdAsync(theLoaiId, ct);
            if (entity is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy Thể Loại Id={TheLoaiId}", theLoaiId);
                throw new TheLoaiNotFoundException(theLoaiId.ToString());
            }

            var dto = _theLoaiResponseMapper.toDto(entity);

            _logger.LogInformation("ApplicationService: Lấy chi tiết Thể Loại Id={TheLoaiId} thành công", theLoaiId);

            return ResultDTO<TheLoaiResponseDTO>.Success(dto, "Lấy chi tiết thể loại thành công");
        }

        public async Task<ResultDTO> CreateTheLoaiAsync(TheLoaiCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu CreateTheLoaiAsync với tên thể loại {TenTL}", createDto.TenTL);

            var theLoai = await _theLoaiOrchestrator.CreateTheLoaiAsync(createDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.TheLoais.CreateAsync(theLoai, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Tạo TheLoai {TenTL} thất bại!", theLoai.TenTL);
                await transaction.RollbackAsync(cancellationToken);
                throw new CreateTheLoaiFailedException(theLoai.TenTL);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Tạo TheLoai {TenTL} thành công!", theLoai.TenTL);
            return ResultDTO.Success($"Tạo thể loại {theLoai.TenTL} thành công");
        }

        public async Task<ResultDTO<UpdateTheLoaiResponseDTO>> UpdateTheLoaiAsync(TheLoaiUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu UpdateTheLoaiAsync với Id={TheLoaiId}", updateDto.TheLoaiId);

            var theLoai = await _theLoaiOrchestrator.UpdateTheLoaiAsync(updateDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.TheLoais.UpdateAsync(theLoai, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Cập nhật TheLoai {TheLoaiId} thất bại!", updateDto.TheLoaiId);
                await transaction.RollbackAsync(cancellationToken);
                throw new UpdateTheLoaiFailedException(theLoai.TenTL);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Cập nhật TheLoai {TheLoaiId} thành công!", theLoai.TheLoaiId);

            var response = _theLoaiResponseMapper.toUpdateDto(theLoai);

            return ResultDTO<UpdateTheLoaiResponseDTO>.Success(
                response,
                $"Cập nhật thể loại {theLoai.TenTL} thành công");
        }

        public async Task<ResultDTO> DeleteTheLoaiAsync(TheLoaiDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu DeleteTheLoaiAsync với Id={TheLoaiId}", deleteDto.TheLoaiId);

            await _theLoaiOrchestrator.DeleteTheLoaiAsync(deleteDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var theLoai = await _unitOfWork.TheLoais.GetByIdAsync(deleteDto.TheLoaiId, cancellationToken);
            if (theLoai is null)
            {
                _logger.LogWarning("ApplicationService: TheLoai không tồn tại khi xóa {TheLoaiId}", deleteDto.TheLoaiId);
                return ResultDTO.Fail($"Không tìm thấy thể loại với Id {deleteDto.TheLoaiId}");
            }

            await _unitOfWork.TheLoais.DeleteAsync(theLoai, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Xóa TheLoai {TheLoaiId} thành công!", deleteDto.TheLoaiId);
            return ResultDTO.Success($"Xóa thể loại {theLoai.TenTL} thành công");
        }
    }
}
