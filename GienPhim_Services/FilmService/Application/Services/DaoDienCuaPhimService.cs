using GienPhim_Services.FilmService.Application.DTOs.Client.DaoDienCuaPhim;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class DaoDienCuaPhimService : IDaoDienCuaPhimService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDaoDienCuaPhimResponseMapper _mapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly IDaoDienCuaPhimOrchestrator _orchestrator;
        private readonly ILogger<DaoDienCuaPhimService> _logger;

        public DaoDienCuaPhimService(
            IUnitOfWork unitOfWork,
            IDaoDienCuaPhimResponseMapper mapper,
            IPaginationMapper paginationMapper,
            IDaoDienCuaPhimOrchestrator orchestrator,
            ILogger<DaoDienCuaPhimService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paginationMapper = paginationMapper;
            _orchestrator = orchestrator;
            _logger = logger;
        }

        public async Task<ResultListDTO<List<DaoDienCuaPhimResponseDTO>>> GetAllAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu GetAllAsync cho DaoDienCuaPhim với page={Page}, limit={Limit}", page, limit);

            var (daoDienCuaPhims, total, last) = await _unitOfWork.DaoDienCuaPhims.GetAllAsync(page, limit, cancellationToken);

            if (total == 0)
            {
                _logger.LogWarning("Service: Không tìm thấy quan hệ Đạo diễn - Phim nào");
                throw new DaoDienCuaPhimNotFoundException(0, 0);
            }

            var dtoList = _mapper.ToDtoList(daoDienCuaPhims);
            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            return ResultListDTO<List<DaoDienCuaPhimResponseDTO>>.Success(dtoList, "Lấy danh sách thành công", pagination);
        }

        public async Task<ResultDTO> CreateAsync(DaoDienCuaPhimCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu CreateAsync DaoDienCuaPhim với PhimId={PhimId}, DaoDienId={DaoDienId}", createDto.PhimId, createDto.DaoDienId);

            var existing = (await _unitOfWork.DaoDienCuaPhims.GetAllAsync(1, int.MaxValue, cancellationToken)).daoDienCuaPhims;

            var daoDienCuaPhim = await _orchestrator.CreateAsync(createDto, existing, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.DaoDienCuaPhims.CreateAsync(daoDienCuaPhim, cancellationToken);
            if (result is null)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Tạo DaoDienCuaPhim thất bại (PhimId={createDto.PhimId}, DaoDienId={createDto.DaoDienId})");
            }

            await transaction.CommitAsync(cancellationToken);

            return ResultDTO.Success($"Thêm đạo diễn vào phim thành công (PhimId={createDto.PhimId}, DaoDienId={createDto.DaoDienId})");
        }

        public async Task<ResultDTO<UpdateDaoDienCuaPhimResponseDTO>> UpdateAsync(DaoDienCuaPhimUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu UpdateAsync DaoDienCuaPhim với PhimId={PhimId}, DaoDienId={DaoDienId}", updateDto.PhimId, updateDto.DaoDienId);

            var existing = await _unitOfWork.DaoDienCuaPhims.GetByIdAsync(updateDto.PhimId, updateDto.DaoDienId, cancellationToken);
            if (existing is null)
            {
                _logger.LogWarning("Service: Không tìm thấy quan hệ PhimId={PhimId}, DaoDienId={DaoDienId}", updateDto.PhimId, updateDto.DaoDienId);
                throw new DaoDienCuaPhimNotFoundException(updateDto.PhimId, updateDto.DaoDienId);
            }

            var daoDienCuaPhim = await _orchestrator.UpdateAsync(updateDto, existing, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.DaoDienCuaPhims.UpdateAsync(daoDienCuaPhim, cancellationToken);
            if (result is null)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Cập nhật DaoDienCuaPhim thất bại (PhimId={updateDto.PhimId}, DaoDienId={updateDto.DaoDienId})");
            }

            await transaction.CommitAsync(cancellationToken);

            var response = _mapper.ToUpdateDto(result);

            return ResultDTO<UpdateDaoDienCuaPhimResponseDTO>.Success(response, "Cập nhật thành công");
        }

        public async Task<ResultDTO> DeleteAsync(DaoDienCuaPhimDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu DeleteAsync DaoDienCuaPhim với PhimId={PhimId}, DaoDienId={DaoDienId}", deleteDto.PhimId, deleteDto.DaoDienId);

            var existingRelation = await _unitOfWork.DaoDienCuaPhims.GetByIdAsync(deleteDto.PhimId, deleteDto.DaoDienId, cancellationToken);
            if (existingRelation is null)
            {
                _logger.LogWarning("Service: Không tìm thấy quan hệ PhimId={PhimId}, DaoDienId={DaoDienId}", deleteDto.PhimId, deleteDto.DaoDienId);
                throw new DaoDienCuaPhimNotFoundException(deleteDto.PhimId, deleteDto.DaoDienId);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            await _unitOfWork.DaoDienCuaPhims.DeleteAsync(existingRelation, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return ResultDTO.Success($"Xóa đạo diễn khỏi phim thành công (PhimId={deleteDto.PhimId}, DaoDienId={deleteDto.DaoDienId})");
        }
    }
}
