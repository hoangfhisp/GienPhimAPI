using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;
using Shared.FileStorage;

namespace GienPhim_Services.FilmService.Application.Services
{
    public class PhimService : IPhimService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhimResponseMapper _phimResponseMapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly IPhimFactory _phimFactory;
        private readonly IPhimDomainService _phimDomainService;
        private readonly ILogger<PhimService> _logger;

        public PhimService(
            IUnitOfWork unitOfWork,
            IPhimResponseMapper phimResponseMapper,
            IPaginationMapper paginationMapper,
            IPhimDomainService phimDomainService,
            IPhimFactory phimFactory,
            ILogger<PhimService> logger)
        {
            _unitOfWork = unitOfWork;
            _phimResponseMapper = phimResponseMapper;
            _paginationMapper = paginationMapper;
            _phimDomainService = phimDomainService;
            _phimFactory = phimFactory;
            _logger = logger;
        }

        public async Task<ResultListDTO<List<PhimResponseDTO>>> GetAllPhimsAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetAllPhimsAsync với page={Page}, limit={Limit}", page, limit);

            var (phims, total, last) = await _unitOfWork.Phims.GetAllPhimsAsync(page, limit, cancellationToken);

            if (total == 0)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy phim nào");
                throw new PhimNotFoundException();
            }

            var resultDtos = _phimResponseMapper.ToDtoList(phims);

            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            _logger.LogInformation("ApplicationService: Lấy danh sách phim thành công, tổng={Total}", total);

            return ResultListDTO<List<PhimResponseDTO>>.Success(resultDtos, "Lấy danh sách phim thành công", pagination);
        }

        public async Task<ResultDTO<PhimResponseDTO>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetByIdAsync với Id={PhimId}", id);

            var phim = await _unitOfWork.Phims.GetByIdAsync(id, ct);
            if (phim is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy Phim với Id={PhimId}", id);
                throw new PhimNotFoundException(id);
            }

            var dto = _phimResponseMapper.ToDto(phim);

            _logger.LogInformation("ApplicationService: Lấy chi tiết Phim Id={PhimId} thành công", id);

            return ResultDTO<PhimResponseDTO>.Success(dto, $"Lấy chi tiết phim Id={id} thành công");
        }

        public async Task<ResultDTO> CreatePhimAsync(PhimCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu CreatePhimAsync với tên {TenP}", createDto.TenP);

            // 1. Xử lý avatar & poster (sử dụng FileHelper)
            string avatarFileName = string.Empty;
            string posterFileName = string.Empty;

            if (createDto.AvatarP != null)
            {
                avatarFileName = await FileHelper.SaveImageAsync(createDto.AvatarP, Path.Combine("wwwroot", "images", "films"));
            }

            if (createDto.AvatarPoster != null)
            {
                posterFileName = await FileHelper.SaveImageAsync(createDto.AvatarPoster, Path.Combine("wwwroot", "images", "banners"));
            }

            // 2. Gọi orchestrator để build entity + validate
            var phim = _phimFactory.CreatePhim(createDto, avatarFileName, posterFileName);
            _phimDomainService.EnsureCanCreate(phim, avatarFileName, posterFileName);

            // 3. Transaction + lưu vào DB
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.Phims.CreateAsync(phim, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Tạo Phim {TenP} thất bại!", phim.TenP);
                await transaction.RollbackAsync(cancellationToken);
                throw new CreatePhimFailedException(phim.TenP);
            }

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("ApplicationService: Tạo Phim {TenP} thành công!", phim.TenP);

            return ResultDTO.Success($"Tạo phim {phim.TenP} thành công");
        }


        public async Task<ResultDTO<UpdatePhimResponseDTO>> UpdatePhimAsync(PhimUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu UpdatePhimAsync với Id={PhimId}", updateDto.PhimId);

            // 1. Lấy phim hiện tại
            var existingPhim = await _unitOfWork.Phims.GetByIdAsync(updateDto.PhimId, cancellationToken);
            if (existingPhim is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy Phim với Id={PhimId}", updateDto.PhimId);
                throw new PhimNotFoundException(updateDto.PhimId);
            }

            // 2. Xử lý avatar & poster (nếu có update)
            string avatarFileName = existingPhim.AvatarP;
            string posterFileName = existingPhim.AvatarPoster;

            if (updateDto.AvatarP != null)
            {
                // Xoá ảnh cũ
                FileHelper.DeleteImage(Path.Combine("wwwroot", "images", "films", existingPhim.AvatarP));
                avatarFileName = await FileHelper.SaveImageAsync(updateDto.AvatarP, Path.Combine("wwwroot", "images", "films"));
            }

            if (updateDto.AvatarPoster != null)
            {
                FileHelper.DeleteImage(Path.Combine("wwwroot", "images", "banners", existingPhim.AvatarPoster));
                posterFileName = await FileHelper.SaveImageAsync(updateDto.AvatarPoster, Path.Combine("wwwroot", "images", "banners"));
            }

            // 3. Factory dựng lại entity mới
            var updatedPhim = _phimFactory.UpdatePhim(updateDto, avatarFileName, posterFileName);

            // 4. Domain validate rule
            _phimDomainService.EnsureCanUpdate(updatedPhim, avatarFileName, posterFileName);

            // 5. Transaction + update DB
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.Phims.UpdateAsync(updatedPhim, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Cập nhật Phim {PhimId} thất bại!", updateDto.PhimId);
                await transaction.RollbackAsync(cancellationToken);
                throw new UpdatePhimFailedException(updatedPhim.TenP);
            }

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("ApplicationService: Cập nhật Phim {PhimId} thành công!", updatedPhim.PhimId);

            // Map sang response
            var response = _phimResponseMapper.ToUpdateDto(updatedPhim);

            return ResultDTO<UpdatePhimResponseDTO>.Success(response, $"Cập nhật phim {updatedPhim.TenP} thành công");
        }

        public async Task<ResultDTO> DeletePhimAsync(PhimDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu DeletePhimAsync với Id={PhimId}", deleteDto.PhimId);

            var phim = await _unitOfWork.Phims.GetByIdAsync(deleteDto.PhimId, cancellationToken);
            if (phim is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy Phim với Id={PhimId}", deleteDto.PhimId);
                throw new PhimNotFoundException(deleteDto.PhimId);
            }

            // Xóa file ảnh vật lý
            FileHelper.DeleteImage(Path.Combine("wwwroot", "images", "films", phim.AvatarP));
            FileHelper.DeleteImage(Path.Combine("wwwroot", "images", "banners", phim.AvatarPoster));

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            await _unitOfWork.Phims.DeleteAsync(phim, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("ApplicationService: Xoá Phim {PhimId} thành công!", deleteDto.PhimId);

            return ResultDTO.Success($"Xoá phim {phim.TenP} thành công");
        }

    }
}
