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
using GienPhim_Services.FilmService.Domain.Entities;

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

            // 1. Xử lý avatar & poster (lưu path tương đối vào DB)
            string avatarPath = string.Empty;
            string posterPath = string.Empty;

            // thư mục vật lý để lưu file
            var filmsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "films");
            var bannersFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "banners");

            if (createDto.AvatarP != null && createDto.AvatarP.Length > 0)
            {
                var fileName = await FileHelper.SaveImageAsync(createDto.AvatarP, filmsFolder);

                // ✅ lưu đường dẫn tương đối (URL path) vào DB
                avatarPath = Path.Combine("images", "films", fileName).Replace("\\", "/");
            }

            if (createDto.AvatarPoster != null && createDto.AvatarPoster.Length > 0)
            {
                var fileName = await FileHelper.SaveImageAsync(createDto.AvatarPoster, bannersFolder);

                // ✅ lưu đường dẫn tương đối (URL path) vào DB
                posterPath = Path.Combine("images", "banners", fileName).Replace("\\", "/");
            }

            // 2. build entity
            var phim = _phimFactory.CreatePhim(createDto, avatarPath, posterPath);

            // GẮN QUAN HỆ N–N (PHẦN BỊ THIẾU)
            phim.PhanLoaiTheLoais = createDto.TheLoaiIds?
                .Distinct()
                .Select(id => new PhanLoaiTheLoai
                {
                    TheLoaiId = id
                })
                .ToList();

            phim.DaoDienCuaPhims = createDto.DaoDienIds?
                .Distinct()
                .Select(id => new DaoDienCuaPhim
                {
                    DaoDienId = id,
                    VaiTroDG = "Đạo diễn chính",  
                    ThoiGianTao = DateTime.UtcNow,
                    ThoiGianCapNhat = DateTime.UtcNow
                })
                .ToList();

            phim.DienVienCuaPhims = createDto.DienVienIds?
                .Distinct()
                .Select(id => new DienVienCuaPhim
                {
                    DienVienId = id
                })
                .ToList();

            phim.PhanLoaiTheTags = createDto.TheTagIds?
                .Distinct()
                .Select(id => new PhanLoaiTheTag
                {
                    TheTagId = id
                })
                .ToList();
            _phimDomainService.EnsureCanCreate(phim, avatarPath, posterPath);

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

            var phim = await _unitOfWork.Phims.GetByIdWithRelationsAsync(updateDto.PhimId, cancellationToken);
            if (phim is null) throw new PhimNotFoundException(updateDto.PhimId);

            // folder vật lý
            var filmsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "films");
            var bannersFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "banners");

            // path DB hiện tại (relative)
            var avatarPath = phim.AvatarP;         // ví dụ: "images/films/xxx.jpg"
            var posterPath = phim.AvatarPoster;    // ví dụ: "images/banners/yyy.png"

            // 1) Nếu upload avatar mới -> xóa ảnh cũ theo đường dẫn vật lý + lưu ảnh mới và set relative path
            if (updateDto.AvatarP != null && updateDto.AvatarP.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(phim.AvatarP))
                {
                    var oldAvatarPhysical = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", phim.AvatarP);
                    FileHelper.DeleteImage(oldAvatarPhysical);
                }

                var fileName = await FileHelper.SaveImageAsync(updateDto.AvatarP, filmsFolder);
                avatarPath = Path.Combine("images", "films", fileName).Replace("\\", "/");
            }

            // 2) Nếu upload poster mới
            if (updateDto.AvatarPoster != null && updateDto.AvatarPoster.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(phim.AvatarPoster))
                {
                    var oldPosterPhysical = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", phim.AvatarPoster);
                    FileHelper.DeleteImage(oldPosterPhysical);
                }

                var fileName = await FileHelper.SaveImageAsync(updateDto.AvatarPoster, bannersFolder);
                posterPath = Path.Combine("images", "banners", fileName).Replace("\\", "/");
            }

            // 3. UPDATE FIELD ĐƠN
            phim.TenP = updateDto.TenP.Trim();
            phim.MoTaP = string.IsNullOrWhiteSpace(updateDto.MoTaP) ? null : updateDto.MoTaP.Trim();
            phim.QuocGiaId = updateDto.QuocGiaId;
            phim.NgonNguId = updateDto.NgonNguId;
            phim.TenCTSXP = updateDto.TenCTSXP.Trim();
            phim.NgayPhatHanhP = updateDto.NgayPhatHanhP;
            phim.ThuoiLuongP = updateDto.ThuoiLuongP;
            phim.NguonP = updateDto.NguonP;
            phim.SoTapP = updateDto.SoTapP;
            phim.TraiLerP = updateDto.TraiLerP;
            phim.AvatarP = avatarPath;
            phim.AvatarPoster = posterPath;
            phim.ThoiGianCapNhat = DateTime.UtcNow;

            // 4. UPDATE QUAN HỆ N–N (CLEAR + REBUILD)

            // Thể loại
            phim.PhanLoaiTheLoais ??= new List<PhanLoaiTheLoai>();
            phim.PhanLoaiTheLoais.Clear();
            if (updateDto.TheLoaiIds != null)
            {
                foreach (var id in updateDto.TheLoaiIds.Distinct())
                {
                    phim.PhanLoaiTheLoais.Add(new PhanLoaiTheLoai
                    {
                        TheLoaiId = id,
                        ThoiGianTao = DateTime.UtcNow,
                        ThoiGianCapNhat = DateTime.UtcNow
                    });
                }
            }

            // Đạo diễn
            phim.DaoDienCuaPhims ??= new List<DaoDienCuaPhim>();
            phim.DaoDienCuaPhims.Clear();
            if (updateDto.DaoDienIds != null)
            {
                foreach (var id in updateDto.DaoDienIds.Distinct())
                {
                    phim.DaoDienCuaPhims.Add(new DaoDienCuaPhim
                    {
                        DaoDienId = id,
                        VaiTroDG = "Đạo diễn chính",
                        ThoiGianTao = DateTime.UtcNow,
                        ThoiGianCapNhat = DateTime.UtcNow
                    });
                }
            }

            // Diễn viên
            phim.DienVienCuaPhims.Clear();
            phim.DienVienCuaPhims ??= new List<DienVienCuaPhim>();
            if (updateDto.DienVienIds != null)
            {
                foreach (var id in updateDto.DienVienIds.Distinct())
                {
                    phim.DienVienCuaPhims.Add(new DienVienCuaPhim
                    {
                        DienVienId = id,
                        ThoiGianTao = DateTime.UtcNow,
                        ThoiGianCapNhat = DateTime.UtcNow
                    });
                }
            }

            // Thẻ tag
            phim.PhanLoaiTheTags.Clear();
            phim.PhanLoaiTheTags ??= new List<PhanLoaiTheTag>();
            if (updateDto.TheTagIds != null)
            {
                foreach (var id in updateDto.TheTagIds.Distinct())
                {
                    phim.PhanLoaiTheTags.Add(new PhanLoaiTheTag
                    {
                        TheTagId = id,
                        ThoiGianTao = DateTime.UtcNow,
                        ThoiGianCapNhat = DateTime.UtcNow
                    });
                }
            }

            // 5. Domain validate
            _phimDomainService.EnsureCanUpdate(phim, avatarPath, posterPath);
            // 6. Save
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.Phims.UpdateAsync(phim, cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("ApplicationService: Cập nhật Phim {PhimId} thành công!", phim.PhimId);

            var response = _phimResponseMapper.ToUpdateDto(phim);

            return ResultDTO<UpdatePhimResponseDTO>.Success(response, $"Cập nhật phim {phim.TenP} thành công");
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

        public async Task<ResultDTO> UpdatePhimByLuotLikeDisLikeAsync(int phimId, bool isLike, bool isDislike, bool? oldIsLike, bool? oldIsDislike, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Update lượt like/dislike cho PhimId={PhimId}", phimId);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var phim = await _unitOfWork.Phims.GetByIdAsync(phimId, cancellationToken);
            if (phim is null)
            {
                _logger.LogWarning("Không tìm thấy Phim với Id={PhimId}", phimId);
                throw new PhimNotFoundException(phimId);
            }

            // HỦY TRẠNG THÁI CŨ
            if (oldIsLike == true)
                phim.LuotThichP = Math.Max(0, phim.LuotThichP - 1);

            if (oldIsDislike == true)
                phim.LuotDislikeP = Math.Max(0, phim.LuotDislikeP - 1);

            // ÁP DỤNG TRẠNG THÁI MỚI
            if (isLike)
                phim.LuotThichP += 1;

            if (isDislike)
                phim.LuotDislikeP += 1;

            phim.ThoiGianCapNhat = DateTime.UtcNow;

            var update = await _unitOfWork.Phims.UpdateAsync(phim, cancellationToken);
            if (update == null)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError("Cập nhật lượt like/dislike thất bại cho PhimId={PhimId}", phimId);
                throw new UpdatePhimLikeDislikeFailedException(phimId);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("Update lượt like/dislike thành công cho PhimId={PhimId}", phimId);
            return ResultDTO.Success("Cập nhật lượt like/dislike cho phim thành công");
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
