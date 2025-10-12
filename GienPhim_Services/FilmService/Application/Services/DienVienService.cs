using GienPhim_Services.FilmService.Application.DTOs.Client.DienVien;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class DienVienService : IDienVienService
    {
        private readonly IDienVienOrchestrator _orchestrator;
        private readonly IDienVienResponseMapper _mapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DienVienService> _logger;

        public DienVienService(
            IUnitOfWork unitOfWork,
            IDienVienOrchestrator orchestrator,
            IDienVienResponseMapper mapper,
            IPaginationMapper paginationMapper,
            ILogger<DienVienService> logger)
        {
            _unitOfWork = unitOfWork;
            _orchestrator = orchestrator;
            _paginationMapper = paginationMapper;
            _mapper = mapper;
            _logger = logger;
        }

        // Lấy danh sách diễn viên (có phân trang)
        public async Task<ResultListDTO<List<DienVienResponseDTO>>> GetAllDienViensAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetAllDienViensAsync với page={Page}, limit={Limit}", page, limit);

            var (dienViens, total, last) = await _unitOfWork.DienViens.GetAllDienViensAsync(page, limit, cancellationToken);

            if (total == 0)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy diễn viên nào");
                throw new DienVienNotFoundException();
            }
            var resultDtos = _mapper.toDtoList(dienViens);

            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            _logger.LogInformation("ApplicationService: Lấy danh sách diễn viên thành công, tổng={Total}", total);

            return ResultListDTO<List<DienVienResponseDTO>>.Success(
                resultDtos,
                "Lấy danh sách diễn viên thành công",
                pagination
            );
        }

        // Xem chi tiết diễn viên
        public async Task<ResultDTO<DienVienResponseDTO>> GetDetailAsync(int dienVienId, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetDetailAsync cho DienVienId={DienVienId}", dienVienId);

            var dienVien = await _unitOfWork.DienViens.GetByIdAsync(dienVienId, ct);
            if (dienVien is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy Diễn viên Id={DienVienId}", dienVienId);
                throw new DienVienNotFoundException(dienVienId.ToString());
            }

            var dto = _mapper.toDto(dienVien);

            _logger.LogInformation("ApplicationService: Lấy chi tiết Diễn viên Id={DienVienId} thành công", dienVienId);

            return ResultDTO<DienVienResponseDTO>.Success(dto, "Lấy chi tiết diễn viên thành công");
        }

        // Tạo mới diễn viên
        public async Task<ResultDTO> CreateDienVienAsync(DienVienCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu CreateDienVienAsync với tên {TenDV}", createDto.TenDV);

            var dienVien = await _orchestrator.CreateDienVienAsync(createDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.DienViens.CreateAsync(dienVien, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Tạo DienVien {TenDV} thất bại!", dienVien.TenDV);
                await transaction.RollbackAsync(cancellationToken);
                throw new CreateDienVienFailedException(dienVien.TenDV);
            }

            await transaction.CommitAsync(cancellationToken);
            return ResultDTO.Success($"Tạo diễn viên {dienVien.TenDV} thành công");
        }

        // Cập nhật diễn viên
        public async Task<ResultDTO<UpdateDienVienResponseDTO>> UpdateDienVienAsync(DienVienUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu UpdateDienVienAsync với Id={Id}", updateDto.DienVienId);

            var dienVien = await _orchestrator.UpdateDienVienAsync(updateDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.DienViens.UpdateAsync(dienVien, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Cập nhật DienVien {Id} thất bại!", updateDto.DienVienId);
                await transaction.RollbackAsync(cancellationToken);
                throw new UpdateDienVienFailedException(dienVien.TenDV);
            }

            await transaction.CommitAsync(cancellationToken);

            var response = _mapper.ToUpdateDto(dienVien);
            return ResultDTO<UpdateDienVienResponseDTO>.Success(
                response,
                $"Cập nhật diễn viên {dienVien.TenDV} thành công"
            );
        }

        // Xóa diễn viên
        public async Task<ResultDTO> DeleteDienVienAsync(DienVienDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu DeleteDienVienAsync với Id={Id}", deleteDto.DienVienId);

            await _orchestrator.DeleteDienVienAsync(deleteDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var dienVien = await _unitOfWork.DienViens.GetByIdAsync(deleteDto.DienVienId, cancellationToken);
            if (dienVien is null)
            {
                _logger.LogWarning("ApplicationService: DienVien không tồn tại khi xóa {Id}", deleteDto.DienVienId);
                return ResultDTO.Fail($"Không tìm thấy diễn viên với Id {deleteDto.DienVienId}");
            }

            await _unitOfWork.DienViens.DeleteAsync(dienVien, cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return ResultDTO.Success($"Xóa diễn viên {dienVien.TenDV} thành công");
        }


        // Tìm kiếm diễn viên theo từ khóa (gần đúng)
        public async Task<ResultListDTO<List<DienVienResponseDTO>>> SearchDienViensAsync(string keyword, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu SearchDienViensAsync với keyword = {Keyword}", keyword);

            var dienViens = await _unitOfWork.DienViens.SearchDienViensAsync(keyword, ct);

            if (dienViens == null || !dienViens.Any())
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy diễn viên nào phù hợp với keyword = {Keyword}", keyword);
                throw new DienVienNotFoundException();
            }

            var resultDtos = _mapper.toDtoList(dienViens);

            _logger.LogInformation("ApplicationService: Tìm kiếm diễn viên thành công, tổng={Count}", resultDtos.Count);

            return ResultListDTO<List<DienVienResponseDTO>>.Success(resultDtos, "Tìm kiếm diễn viên thành công");
        }
    }
}
