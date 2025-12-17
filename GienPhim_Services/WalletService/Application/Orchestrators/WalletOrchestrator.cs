using GienPhim_Services.WalletService.Application.DTOs.Common;
using GienPhim_Services.WalletService.Application.DTOs.User;
using GienPhim_Services.WalletService.Application.DTOs.Wallets;
using GienPhim_Services.WalletService.Application.Exceptions.Failed;
using GienPhim_Services.WalletService.Application.Exceptions.NotFounds;
using GienPhim_Services.WalletService.Application.Interfaces.Factories;
using GienPhim_Services.WalletService.Application.Interfaces.Orchestrators;
using GienPhim_Services.WalletService.Application.Interfaces.Validators;
using GienPhim_Services.WalletService.Domain.Entities;
using GienPhim_Services.WalletService.Domain.Interfaces.Services;
using GienPhim_Services.WalletService.Infrastructure.HttpClients.Interfaces;

namespace GienPhim_Services.WalletService.Application.Orchestrators
{
    public class WalletOrchestrator : IWalletOrchestrator
    {
        private readonly IValidationService _validationService;
        private readonly IWalletDomainService _walletDomainService;
        private readonly IWalletFactory _walletFactory;
        private readonly IWalletTransactionFactory _walletTransactionFactory;
        private readonly IUserServiceClient _userServiceClient;
        private readonly ILogger<WalletOrchestrator> _logger;

        public WalletOrchestrator(IValidationService validationService, IWalletDomainService walletDomainService
            , IWalletFactory walletFactory, IWalletTransactionFactory walletTransactionFactory
            , IUserServiceClient userServiceClient
            , ILogger<WalletOrchestrator> logger)
        {
            _validationService = validationService;
            _walletDomainService = walletDomainService;
            _walletFactory = walletFactory;
            _walletTransactionFactory = walletTransactionFactory;
            _userServiceClient = userServiceClient;
            _logger = logger;
        }

        // Tạo ví mới
        public async Task<Wallet> CreateWalletAsync(CreateWalletRequestDTO dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateWalletAsync với userId = {UserId}", dto.UserId);

            // Check userId có tồn tại không
            await FindUserByIdAsync(dto.UserId, cancellationToken);

            // Validate
            await _validationService.ValidateAsync(dto,dto.UserId, cancellationToken);

            var walletFactory = _walletFactory.CreateWallet(dto);

            _walletDomainService.EnsureCanCreate(walletFactory);

            _logger.LogInformation("Orchestrator: Tạo ví thành công cho userId = {UserId}", dto.UserId);
            return walletFactory;

        }

        public async Task<Wallet> UpdateWalletAsync(UpdateWalletRequestDTO dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateWalletAsync với userId = {UserId}", dto.UserId);

            // Check userId có tồn tại không
            await FindUserByIdAsync(dto.UserId, cancellationToken);

            // Validate
            await _validationService.ValidateAsync(dto, cancellationToken);

            var walletFactory = _walletFactory.UpdateWallet(dto);

            _walletDomainService.EnsureCanUpdate(walletFactory);

            _logger.LogInformation("Orchestrator: Update ví thành công cho userId = {UserId}", dto.UserId);
            return walletFactory;

        }

        // Tạo ví mới
        public async Task<WalletTransaction> CreateWalletTransactionAsync(CreateWalletTransactionRequestDTO dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateWalletTransactionAsync với walletId = {WalletId}", dto.WalletId);

            // Validate
            var soDuSauGiaoDich = await _validationService.ValidateAsync<CreateWalletTransactionRequestDTO,decimal>(dto, cancellationToken);

            var walletTransactionFactory = _walletTransactionFactory.Create(dto,soDuSauGiaoDich);

            _walletDomainService.EnsureCanCreateTransaction(walletTransactionFactory);

            _logger.LogInformation("Orchestrator: Tạo giao dịch ví thành công cho walletId = {WalletId}", dto.WalletId);
            return walletTransactionFactory;

        }

        //private methods
        // Tìm user theo id bằng UserService
        public async Task<ResultDTO<UserHttpResponseDTO>?> FindUserByIdAsync (string userId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu FindUserById với userId = {UserId}", userId);
            var user = await _userServiceClient.FindUserByUserIdAsync(userId, cancellationToken);

            // Không kết nối được đến UserService
            if (user == null )
            {
                _logger.LogError("Orchestrator: FindUserById không thể kết nối đến UserService với userId = {UserId}", userId);
                throw new ConnectServiceFailedException("UserService");
            }

            // Không tìm thấy user
            if (!user.Data.Status)
            {
                _logger.LogWarning("Orchestrator: FindUserById không tìm thấy userId = {UserId}, Lỗi: {Error}", userId, user.Data.Msg);
                throw new UserNotFoundException(user.Data.Msg,user.StatusCode);
            }

            _logger.LogInformation("Orchestrator: FindUserById tìm thấy userId = {UserId}", userId);
            return user.Data;
        }

    }
}
