using GienPhim_Services.WalletService.Application.DTOs.Common;
using GienPhim_Services.WalletService.Application.DTOs.Wallets;
using GienPhim_Services.WalletService.Application.Exceptions.Failed;
using GienPhim_Services.WalletService.Application.Exceptions.NotFounds;
using GienPhim_Services.WalletService.Application.Interfaces.Mappers;
using GienPhim_Services.WalletService.Application.Interfaces.Orchestrators;
using GienPhim_Services.WalletService.Application.Interfaces.Repositories;
using GienPhim_Services.WalletService.Application.Interfaces.Services;


namespace GienPhim_Services.WalletService.Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletOrchestrator _walletOrchestrator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpUserContextAccessor _httpUserContextAccessor;
        private readonly IWalletResponseMapper _walletResponseMapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly ILogger<WalletService> _logger;

        public WalletService(IWalletOrchestrator walletOrchestrator,IUnitOfWork unitOfWork
            , IHttpUserContextAccessor httpUserContextAccessor, IWalletResponseMapper walletResponseMapper
            ,IPaginationMapper paginationMapper, ILogger<WalletService> logger)
        {
            _walletOrchestrator = walletOrchestrator;
            _unitOfWork = unitOfWork;
            _httpUserContextAccessor = httpUserContextAccessor;
            _walletResponseMapper = walletResponseMapper;
            _paginationMapper = paginationMapper;
            _logger = logger;
        }

        // Lấy thông tin ví 
        public async Task<ResultDTO<WalletResponseDTO>> GetWalletByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu GetWalletByUserIdAsync cho userId = {UserId}", userId);
            var wallet = await _unitOfWork.Wallet.FindWalletByUserIdAsync(userId, cancellationToken);
            if (wallet == null)
            {
                _logger.LogWarning("Service: Không tìm thấy ví cho userId = {UserId}", userId);
                throw new WalletNotFoundException(userId);
            }
            _logger.LogInformation("Service: Lấy thông tin ví thành công cho userId = {UserId}", userId);
            return ResultDTO<WalletResponseDTO>.Success(_walletResponseMapper.ToDTO(wallet), "Lấy thông tin ví thành công cho userId: " + userId);
        }

        // Tạo ví mới
        public async Task<ResultDTO<WalletResponseDTO>> CreateWalletAsync(CreateWalletRequestDTO dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu CreateWalletAsync ");

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var wallet = await _walletOrchestrator.CreateWalletAsync(dto , cancellationToken);

            var addWallet = await _unitOfWork.Wallet.AddAsync(wallet, cancellationToken);
            if(addWallet == null)
            {
                _logger.LogWarning("Service: Tạo ví thất bại cho userId = {UserId}", dto.UserId);
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception("Tạo ví thất bại");
            }


            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("Service: Tạo ví thành công cho userId = {UserId}", dto.UserId);
            return ResultDTO<WalletResponseDTO>.Success(_walletResponseMapper.ToDTO(addWallet),"Tạo wallet thành công");
        }

        // Lấy giao dich từ ví theo walletId với phân trang
        public async Task<ResultListDTO<WalletTransactionResponseDTO>> GetWalletTransactionByWalletId(string walletId, int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu GetWalletTransactionByWalletId cho walletId = {WalletId}, page={Page}, limit={Limit}", walletId, page, limit);
            var (wlTransaction, total, last) = await _unitOfWork.WalletTransaction.GetByWalletIdAsync(walletId, page, limit, cancellationToken);
            if (wlTransaction == null || !wlTransaction.Any() || total == 0)
            {
                _logger.LogWarning("Service: Không tìm thấy giao dịch ví cho walletId = {WalletId}", walletId);
                throw new WalletTransactionNotFoundException(walletId);
            }

            var resultMapper = wlTransaction.Select(wlt => _walletResponseMapper.ToTransactionDTO(wlt)).ToList();
            var pagination = _paginationMapper.ToDto(page,limit, total, last);

            _logger.LogInformation("Service: Lấy giao dịch ví thành công cho walletId = {WalletId}, Tổng bản ghi={Total}", walletId, total);
            return ResultListDTO<WalletTransactionResponseDTO>.Success(resultMapper, "Lấy giao dịch ví thành công cho walletId: " + walletId, pagination);
        }

        // Xử lý giao dịch ví
        public async Task<ResultDTO> CreateWalletTransactionAsync(CreateWalletTransactionRequestDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service: Bắt đầu CreateWalletTransactionAsync  cho walletId = {WalletId}", request.WalletId);
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            var walletTransaction = await _walletOrchestrator.CreateWalletTransactionAsync(request, cancellationToken);

            var addWalletTransaction = await _unitOfWork.WalletTransaction.CreateAsync(walletTransaction, cancellationToken);
            if(addWalletTransaction == null)
            {
                _logger.LogWarning("Service: Tạo giao dịch ví thất bại cho walletId = {WalletId}", request.WalletId);
                await transaction.RollbackAsync(cancellationToken);
                throw new CreateWalletTransactionFailedException(addWalletTransaction.WalletId);
            }

            var wallet = await _unitOfWork.Wallet.FindWalletByWalletIdAsync(request.WalletId, cancellationToken);
            if(wallet == null)
            {
                _logger.LogWarning("Service: Không tìm thấy ví cho walletId = {WalletId}", request.WalletId);
                await transaction.RollbackAsync(cancellationToken);
                throw new WalletNotFoundException(request.WalletId);
            }

            wallet.SoDu = walletTransaction.SoDuSauGiaoDich;
            wallet.NgayCapNhat = DateTime.UtcNow;
            var updateWallet = await _unitOfWork.Wallet.UpdateAsync(wallet, cancellationToken);
            if(updateWallet == null)
            {
                _logger.LogWarning("Service: Cập nhật số dư ví thất bại cho walletId = {WalletId}", request.WalletId);
                await transaction.RollbackAsync(cancellationToken);
                throw new UpdateWalletFailedException(wallet.WalletId);
            }
            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("Service: Tạo giao dịch ví thành công cho walletId = {WalletId}", request.WalletId);
            return ResultDTO.Success("Tạo giao dịch ví thành công số dư còn: " + updateWallet.SoDu);
        }
    }
}
