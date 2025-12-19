using GienPhim_Services.FilmService.Application.DTOs.Client.LichSuXem;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Domain.Entities;
using System.Collections.Generic;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class LichSuXemService : ILichSuXemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILichSuXemOrchestrator _lichSuXemOrchestrator;
        private readonly ILichSuXemResponseMapper _lichSuXemResponseMapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly ILogger<LichSuXemService> _logger;

        public LichSuXemService(IUnitOfWork unitOfWork, ILichSuXemOrchestrator lichSuXemOrchestrator
            , ILichSuXemResponseMapper lichSuXemResponseMapper,
            IPaginationMapper paginationMapper, ILogger<LichSuXemService> logger)
        {
            _unitOfWork = unitOfWork;
            _lichSuXemOrchestrator = lichSuXemOrchestrator;
            _lichSuXemResponseMapper = lichSuXemResponseMapper;
            _paginationMapper = paginationMapper;
            _logger = logger;
        }

        // Lấy lịch sử xem theo UserId với phân trang
        public async Task<ResultListDTO<List<LichSuXemResponseDTO>>> GetByUserIdAsync(GetLichSuXemUserRequestDTO request, CancellationToken cancellationToken = default) 
        {
            _logger.LogInformation("LichSuXemService: Bắt đầu lấy lịch sử xem cho UserId={UserId}, page={Page}, limit={Limit}", request.UserId, request.page, request.limit);
            var userId = _lichSuXemOrchestrator.FetchUserByUserIdAsync(request.UserId, cancellationToken);

            var (lichSuXems,total,last) = await _unitOfWork.LichSuXem.GetUserLichSuXem(request.UserId, request.page, request.limit, cancellationToken);
            if(lichSuXems == null || !lichSuXems.Any())
            {
                _logger.LogInformation("LichSuXemService: Không tìm thấy lịch sử xem cho UserId={UserId}", request.UserId);
                throw new LichSuXemNotFoundException(request.UserId);
            }

            foreach (var lsx in lichSuXems)
            {
                var phim = await _unitOfWork.Phims.GetByIdAsync(lsx.PhimId, cancellationToken);
                if (phim != null)
                {
                    lsx.Phim = phim;
                }
            }
            var lichSuXemDtos = lichSuXems.Select(lsx => _lichSuXemResponseMapper.toDTO(lsx,lsx.Phim)).ToList();
            var pagination = _paginationMapper.ToDto(request.page, request.limit, total, last);
            _logger.LogInformation("LichSuXemService: Lấy lịch sử xem thành công cho UserId={UserId}, Tổng bản ghi={Total}", request.UserId, total);
            return ResultListDTO<List<LichSuXemResponseDTO>>.Success(lichSuXemDtos, "Lấy lịch sử xem thành công cho UserId:" + request.UserId, pagination);
        }

        // Xử lý lịch sử xem
        public async Task<ResultDTO> XuLyLichSuXemAsync(LichSuXemCreateDTO createDTO, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Bắt đầu xử lý lịch sử xem UserId={UserId}, PhimId={PhimId}", createDTO.UserId, createDTO.PhimId);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var checkLsx = await _unitOfWork.LichSuXem.CheckLichSuXemAsync(createDTO.PhimId, createDTO.UserId);
  
            if (checkLsx != null)
            {
               
                if (checkLsx.TapVuaXem == createDTO.TapVuaXem)
                {
                    _logger.LogInformation("Lịch sử xem không thay đổi");
                    await transaction.CommitAsync(cancellationToken);
                    return ResultDTO.Success("Lịch sử xem không thay đổi");
                }

                checkLsx.TapVuaXem = createDTO.TapVuaXem;
                checkLsx.ThoiGianXem = DateTime.UtcNow;

                var update = await _unitOfWork.LichSuXem.UpdateLichSuXemAsync(checkLsx);

                if (update == null)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw new UpdateLichSuXemFailedException(
                        createDTO.UserId, createDTO.PhimId);
                }
            }
            else
            {
                var lichSuXem =await _lichSuXemOrchestrator.CreateLichSuXem(createDTO, cancellationToken);

                var create = await _unitOfWork.LichSuXem.AddLichSuXemAsync(lichSuXem);

                if (create == null)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw new CreateLichXuXemFailedException(createDTO.PhimId);
                }
            }

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("Xử lý lịch sử xem thành công");
            return ResultDTO.Success("Xử lý lịch sử xem thành công");
        }
    } 
}
