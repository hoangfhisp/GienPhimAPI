using GienPhim_Services.FilmService.Application.DTOs.Client.DienVienCuaPhim;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Services;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class DienVienCuaPhimService : IDienVienCuaPhimService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDienVienCuaPhimResponseMapper _mapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly IDienVienCuaPhimOrchestrator _orchestrator;
        private readonly ILogger<DienVienCuaPhimService> _logger;

        public DienVienCuaPhimService(
            IUnitOfWork unitOfWork,
            IDienVienCuaPhimResponseMapper mapper,
            IPaginationMapper paginationMapper,
            IDienVienCuaPhimOrchestrator orchestrator,
            ILogger<DienVienCuaPhimService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paginationMapper = paginationMapper;
            _orchestrator = orchestrator;
            _logger = logger;
        }

        public async Task<ResultListDTO<List<DienVienCuaPhimResponseDTO>>> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu GetAllAsync cho DienVienCuaPhim page={Page}, limit={Limit}", page, limit);

            var (relations, total, last) = await _unitOfWork.DienVienCuaPhims.GetAllAsync(page, limit, cancellationToken);

            if (total == 0)
            {
                _logger.LogWarning("Service: Không tìm thấy quan hệ Diễn viên - Phim nào");
                throw new DienVienCuaPhimNotFoundException();
            }

            var dtoList = _mapper.ToDtoList(relations);
            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            return ResultListDTO<List<DienVienCuaPhimResponseDTO>>.Success(dtoList, "Lấy danh sách thành công", pagination);
        }

        public async Task<ResultDTO> CreateAsync(DienVienCuaPhimCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu CreateAsync cho PhimId={PhimId}, DienVienId={DienVienId}", createDto.PhimId, createDto.DienVienId);

            var existingRelations = (await _unitOfWork.DienVienCuaPhims.GetAllAsync(1, int.MaxValue, cancellationToken)).relations;

            var entity = await _orchestrator.CreateAsync(createDto, existingRelations, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.DienVienCuaPhims.CreateAsync(entity, cancellationToken);
            if (result is null)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new CreateDienVienCuaPhimFailedException(createDto.PhimId, createDto.DienVienId);
            }

            await transaction.CommitAsync(cancellationToken);

            return ResultDTO.Success($"Thêm diễn viên vào phim thành công (PhimId={createDto.PhimId}, DienVienId={createDto.DienVienId})");
        }

        public async Task<ResultDTO<UpdateDienVienCuaPhimResponseDTO>> UpdateAsync(DienVienCuaPhimUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu UpdateAsync cho PhimId={PhimId}, DienVienId={DienVienId}", updateDto.PhimId, updateDto.DienVienId);

            var existingRelation = await _unitOfWork.DienVienCuaPhims.GetByIdAsync(updateDto.PhimId, updateDto.DienVienId, cancellationToken);
            if (existingRelation is null)
            {
                _logger.LogWarning("Service: Không tìm thấy quan hệ PhimId={PhimId}, DienVienId={DienVienId}", updateDto.PhimId, updateDto.DienVienId);
                throw new DienVienCuaPhimNotFoundException(updateDto.PhimId, updateDto.DienVienId);
            }

            var entity = await _orchestrator.UpdateAsync(updateDto, existingRelation, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.DienVienCuaPhims.UpdateAsync(entity, cancellationToken);
            if (result is null)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new UpdateDienVienCuaPhimFailedException(updateDto.PhimId, updateDto.DienVienId);
            }

            await transaction.CommitAsync(cancellationToken);

            var response = _mapper.ToUpdateDto(result);

            return ResultDTO<UpdateDienVienCuaPhimResponseDTO>.Success(response, "Cập nhật thành công");
        }

        public async Task<ResultDTO> DeleteAsync(DienVienCuaPhimDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu DeleteAsync cho PhimId={PhimId}, DienVienId={DienVienId}", deleteDto.PhimId, deleteDto.DienVienId);

            var existingRelation = await _unitOfWork.DienVienCuaPhims.GetByIdAsync(deleteDto.PhimId, deleteDto.DienVienId, cancellationToken);
            if (existingRelation is null)
            {
                _logger.LogWarning("Service: Không tìm thấy quan hệ PhimId={PhimId}, DienVienId={DienVienId}", deleteDto.PhimId, deleteDto.DienVienId);
                throw new DienVienCuaPhimNotFoundException(deleteDto.PhimId, deleteDto.DienVienId);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            await _unitOfWork.DienVienCuaPhims.DeleteAsync(existingRelation, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return ResultDTO.Success($"Xóa diễn viên khỏi phim thành công (PhimId={deleteDto.PhimId}, DienVienId={deleteDto.DienVienId})");
        }
    }
}
