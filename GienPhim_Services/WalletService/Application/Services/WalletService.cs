using GienPhim_Services.WalletService.Application.DTOs.Common;
using GienPhim_Services.WalletService.Application.DTOs.Wallets;
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
        private readonly ILogger<WalletService> _logger;

        public WalletService(IWalletOrchestrator walletOrchestrator,IUnitOfWork unitOfWork
            , IHttpUserContextAccessor httpUserContextAccessor, IWalletResponseMapper walletResponseMapper
            , ILogger<WalletService> logger)
        {
            _walletOrchestrator = walletOrchestrator;
            _unitOfWork = unitOfWork;
            _httpUserContextAccessor = httpUserContextAccessor;
            _walletResponseMapper = walletResponseMapper;
            _logger = logger;
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
    }
}
