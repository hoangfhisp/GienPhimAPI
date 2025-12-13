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
        private readonly IUserServiceClient _userServiceClient;
        private readonly ILogger<WalletOrchestrator> _logger;

        public WalletOrchestrator(IValidationService validationService, IWalletDomainService walletDomainService
            , IWalletFactory walletFactory, IUserServiceClient userServiceClient
            , ILogger<WalletOrchestrator> logger)
        {
            _validationService = validationService;
            _walletDomainService = walletDomainService;
            _walletFactory = walletFactory;
            _userServiceClient = userServiceClient;
            _logger = logger;
        }

        // Tạo ví mới
        public async Task<Wallet> CreateWalletAsync(CreateWalletRequestDTO dto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateWalletAsync với userId = {UserId}", dto.UserId);

            // Check userId có tồn tại không
            await FindUserById(dto.UserId, cancellationToken);

            // Validate
            await _validationService.ValidateAsync(dto,dto.UserId, cancellationToken);

            var walletFactory = _walletFactory.CreateWallet(dto);

            _walletDomainService.EnsureCanCreate(walletFactory);

            _logger.LogInformation("Orchestrator: Tạo ví thành công cho userId = {UserId}", dto.UserId);
            return walletFactory;

        }

        //private methods
        // Tìm user theo id bằng UserService
        private async Task<ResultDTO<UserHttpResponseDTO>?> FindUserById (string userId, CancellationToken cancellationToken = default)
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
