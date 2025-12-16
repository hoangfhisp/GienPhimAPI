using GienPhim_Services.FilmService.Application.DTOs.Client.Phim;
using GienPhim_Services.FilmService.Application.DTOs.Common;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Mappers;
using GienPhim_Services.FilmService.Application.Interfaces.Repositories;
using GienPhim_Services.FilmService.Application.Interfaces.Services;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
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
        private readonly IApiPhimFactory apiPhimFactory;
        private readonly IApiPhimOrchestrator _orchestrator;
        private readonly ILogger<PhimService> _logger;

        public PhimService(
            IUnitOfWork unitOfWork,
            IPhimResponseMapper phimResponseMapper,
            IPaginationMapper paginationMapper,
            IPhimDomainService phimDomainService,
            IPhimFactory phimFactory,
            IApiPhimOrchestrator orchestrator,
            IApiPhimFactory apiPhimFactory,
            ILogger<PhimService> logger)
        {
            _unitOfWork = unitOfWork;
            _phimResponseMapper = phimResponseMapper;
            _paginationMapper = paginationMapper;
            _phimDomainService = phimDomainService;
            _phimFactory = phimFactory;
            _orchestrator = orchestrator;
            this.apiPhimFactory = apiPhimFactory;
            _logger = logger;
        }

        // Lấy tất cả phim với phân trang
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

        // Lấy phim mới nhất với phân trang
        public async Task<ResultListDTO<List<PhimResponseDTO>>> GetNewPhimsAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetNewPhimsAsync với page={Page}, limit={Limit}", page, limit);
            var (phims, total, last) = await _unitOfWork.Phims.GetNewPhimAsync(page, limit, cancellationToken);
            if (total == 0)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy phim mới nào");
                throw new PhimNotFoundException();
            }

            var resultDtos = _phimResponseMapper.ToDtoList(phims);
            var pagination = _paginationMapper.ToDto(page, limit, total, last);
            _logger.LogInformation("ApplicationService: Lấy danh sách phim mới thành công, tổng={Total}", total);
            return ResultListDTO<List<PhimResponseDTO>>.Success(resultDtos, "Lấy danh sách phim mới thành công", pagination);
        }

        // Lấy phim top view 
        public async Task<ResultListDTO<List<PhimResponseDTO>>> GetTopViewPhimsAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetTopViewPhimsAsync với page={Page}, limit={Limit}", page, limit);
            var (phims, total, last) = await _unitOfWork.Phims.GetTopViewPhimAsync(page, limit, cancellationToken);
            if (total == 0)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy phim top view nào");
                throw new PhimNotFoundException();
            }

            var resultDtos = _phimResponseMapper.ToDtoList(phims);
            var pagination = _paginationMapper.ToDto(page, limit, total, last);
            _logger.LogInformation("ApplicationService: Lấy danh sách phim top view thành công, tổng={Total}", total);
            return ResultListDTO<List<PhimResponseDTO>>.Success(resultDtos, "Lấy danh sách phim top view thành công", pagination);
        }

        // Lấy chi tiết phim theo Id
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

        // Tạo mới phim
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

        // Cập nhật phim
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

        // Xoá phim
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

        // Import phim từ API ngoài
        public async Task<ResultDTO<PhimResponseDTO>> ImportPhimFromApiAsync(string slug, CancellationToken cancellationToken = default)
        {
            // Chuẩn hóa slug ngay đầu (nhận cả URL, tên phim có dấu, slug…)
            var normalizedSlug = SlugHelper.ExtractSlug(slug);

            _logger.LogInformation(
                "ApplicationService: Bắt đầu ImportPhimFromApiAsync với slug raw={RawSlug}, normalized={Slug}",
                slug, normalizedSlug);

            // 1️ Lấy dữ liệu phim từ API thông qua orchestrator
            var phim = await _orchestrator.ProcessImportAsync(normalizedSlug, cancellationToken);
            if (phim is null)
            {
                _logger.LogWarning("ApplicationService: Không thể tạo entity phim từ API slug={Slug}", normalizedSlug);
                throw new PhimNotFoundException($"Không thể lấy dữ liệu phim từ slug '{normalizedSlug}'.");
            }

            // 2️ Kiểm tra phim đã tồn tại chưa (theo slug hoặc tên)
            var existing = await _unitOfWork.Phims.GetBySlugAsync(phim.NguonP, cancellationToken);
            if (existing != null)
            {
                _logger.LogWarning("ApplicationService: Phim với slug={Slug} đã tồn tại", normalizedSlug);
                throw new CreatePhimFailedException($"Phim '{phim.TenP}' đã tồn tại trong hệ thống.");
            }

            // 3️ Bắt đầu transaction
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                _logger.LogInformation("ApplicationService: Bắt đầu lưu phim {TenP} vào DB", phim.TenP);

                var createdPhim = await _unitOfWork.Phims.CreateAsync(phim, cancellationToken);
                if (createdPhim == null)
                {
                    _logger.LogError("ApplicationService: Lưu phim {TenP} thất bại!", phim.TenP);
                    await transaction.RollbackAsync(cancellationToken);
                    throw new CreatePhimFailedException(phim.TenP);
                }

                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation("ApplicationService: Import phim {TenP} thành công!", phim.TenP);

                var dto = _phimResponseMapper.ToDto(createdPhim);
                return ResultDTO<PhimResponseDTO>.Success(dto, $"Import phim '{createdPhim.TenP}' thành công");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "ApplicationService: Import phim {Slug} thất bại!", normalizedSlug);
                throw new CreatePhimFailedException($"Import phim thất bại: {ex.Message}");
            }
        }

        // Cập nhật phim từ API ngoài
        public async Task<ResultDTO<PhimResponseDTO>> UpdatePhimFromApiAsync(string slug, CancellationToken cancellationToken = default)
        {
            // Chuẩn hóa slug giống ImportPhimFromApiAsync
            var normalizedSlug = SlugHelper.ExtractSlug(slug);

            _logger.LogInformation("ApplicationService: Bắt đầu UpdatePhimFromApiAsync với slug raw={RawSlug}, normalized={Slug}", slug, normalizedSlug);

            // 1️ Lấy phim hiện có trong DB theo slug đã chuẩn hóa (NguonP)
            var existing = await _unitOfWork.Phims.GetBySlugAsync(normalizedSlug, cancellationToken);
            if (existing == null)
            {
                _logger.LogWarning("ApplicationService: Phim với slug={Slug} không tồn tại", normalizedSlug);
                throw new PhimNotFoundException($"Phim slug '{normalizedSlug}' không tồn tại trong hệ thống.");
            }

            // 2️ Bắt đầu transaction để đảm bảo an toàn dữ liệu
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                // 3️ Gọi orchestrator xử lý cập nhật từ API
                //    (orchestrator bên trong cũng nên dùng normalizedSlug để gọi API ngoài)
                var updatedPhim = await _orchestrator.ProcessUpdateAsync(normalizedSlug, cancellationToken);

                _logger.LogInformation("ApplicationService: Đã xử lý cập nhật phim {TenP} từ API", updatedPhim.TenP);

                // 4️ Lưu thay đổi xuống DB
                await _unitOfWork.Phims.UpdateAsync(updatedPhim, cancellationToken);

                // Nếu UnitOfWork của bạn có Commit riêng thì giữ lại, còn nếu SaveChanges đã được gọi trong Repo rồi thì bỏ CommitAsync này đi
                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation("ApplicationService: Cập nhật phim {TenP} từ API thành công!", updatedPhim.TenP);

                // 5️ Trả kết quả cho client
                var dto = _phimResponseMapper.ToDto(updatedPhim);
                return ResultDTO<PhimResponseDTO>.Success(dto, $"Cập nhật phim '{updatedPhim.TenP}' thành công từ API.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "ApplicationService: Lỗi khi cập nhật phim {Slug} từ API", normalizedSlug);
                throw new UpdatePhimFailedException($"Cập nhật phim thất bại: {ex.Message}");
            }
        }

        // Tìm kiếm phim theo từ khóa
        public async Task<ResultDTO<List<PhimResponseDTO>>> SearchPhimsAsync(string keyword,int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu SearchPhimsAsync với từ khóa {Keyword}", keyword);

            // 1️ Gọi repository
            var (phims,total,last) = await _unitOfWork.Phims.SearchPhimsAsync(keyword,page,limit,cancellationToken);

            if (phims == null || !phims.Any() || total == 0)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy phim nào với từ khóa {Keyword}", keyword);
                throw new PhimNotFoundException();
            }

            // 2️ Map sang DTO
            var dtoList = phims.Select(_phimResponseMapper.ToDto).ToList();

            _logger.LogInformation("ApplicationService: Tìm thấy {Count} phim với từ khóa {Keyword}", dtoList.Count, keyword);

            // 3️ Trả kết quả
            return ResultDTO<List<PhimResponseDTO>>.Success(dtoList, "Tìm kiếm phim thành công");
        }

        // Lọc phim theo nhiều tiêu chí
        public async Task<ResultListDTO<List<PhimResponseDTO>>> FilterPhimsAsync(FilterPhimDTO dto, int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu FilterPhimsAsync với filter {@Dto}, page={Page}, limit={Limit}", dto, page, limit);

            var (phims, total, last) = await _unitOfWork.Phims.FilterPhimsAsync(dto.NgonNguSlug, dto.QuocGiaSlug, dto.TheLoaiSlug,dto.TenDienVien, dto.TenDaoDien, page, limit, cancellationToken);

            if (total == 0)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy phim nào phù hợp filter {@Dto}", dto);
                throw new PhimNotFoundException();
            }

            var resultDtos = _phimResponseMapper.ToDtoList(phims);
            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            _logger.LogInformation("ApplicationService: Lọc phim thành công, tổng={Total}", total);

            return ResultListDTO<List<PhimResponseDTO>>.Success(resultDtos, "Lọc phim thành công", pagination);
        }
    }
}
