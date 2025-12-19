using GienPhim_Services.MembershipService.Application.DTOs.Client.UserMembership;
using GienPhim_Services.MembershipService.Application.DTOs.Common;
using GienPhim_Services.MembershipService.Application.DTOs.Http;
using GienPhim_Services.MembershipService.Application.DTOs.Wallets;
using GienPhim_Services.MembershipService.Application.Exceptions.Invalids;
using GienPhim_Services.MembershipService.Application.Interfaces.Factories;
using GienPhim_Services.MembershipService.Application.Interfaces.Orchestrators;
using GienPhim_Services.MembershipService.Domain.Entities;
using GienPhim_Services.MembershipService.Domain.Services.Interfaces;
using GienPhim_Services.MembershipService.Infrastructure.HttpClients.Interfaces;
using GienPhim_Services.UserService.Application.Exceptions.Failed;

namespace GienPhim_Services.MembershipService.Application.Orchestrators
{
    public class UserMembershipOrchestrator : IUserMembershipOrchestrator
    {
        private readonly IUserMembershipFactory _userMembershipFactory;
        private readonly IUserMembershipDomainService _userMembershipDomainService;
        private readonly IWalletServiceClient _walletServiceClient;
        private readonly ILogger<UserMembershipOrchestrator> _logger;

        public UserMembershipOrchestrator(
            IUserMembershipFactory userMembershipFactory,
            IUserMembershipDomainService userMembershipDomainService,
            IWalletServiceClient walletServiceClient,
            ILogger<UserMembershipOrchestrator> logger)
        {
            _userMembershipFactory = userMembershipFactory;
            _userMembershipDomainService = userMembershipDomainService;
            _walletServiceClient = walletServiceClient;
            _logger = logger;
        }

        // Tạo mới UserMembership
        public async Task<(UserMembership userMembership,string walletId)> CreateUserMembershipAsync(UserMembershipCreateDTO createDto,decimal giaOption,int day , CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateUserMembershipAsync cho UserId={UserId}, OptionId={OptionId}", createDto.UserId, createDto.OptionId);

            var wallet = await GetWalletByUserIdAsync(createDto.UserId, cancellationToken);
            if (wallet.Data.SoDu < giaOption)
            {
                throw new InvalidAmountException();
            }
            
            // Factory khởi tạo entity từ DTO
            var userMembership = _userMembershipFactory.CreateUserMembership(createDto,day);

            // Domain kiểm tra nghiệp vụ
            _userMembershipDomainService.EnsureCanCreate(userMembership);
            
            _logger.LogInformation("Orchestrator: UserMembership cho UserId={UserId} đã được validate và tạo entity thành công", createDto.UserId);
            return (userMembership,wallet.Data.WalletId);
        }

        // Cập nhật UserMembership
        public async Task<UserMembership> UpdateUserMembershipAsync(UserMembershipUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateUserMembershipAsync với UserMembershipId={UserMembershipId}", updateDto.UserMembershipId);

            var userMembership = _userMembershipFactory.UpdateUserMembership(updateDto);

            _userMembershipDomainService.EnsureCanUpdate(userMembership);

            _logger.LogInformation("Orchestrator: UserMembership {UserMembershipId} đã được cập nhật thành công", updateDto.UserMembershipId);
            return userMembership;
        }

        // Xoá UserMembership
        public async Task DeleteUserMembershipAsync(UserMembershipDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteUserMembershipAsync với UserMembershipId={UserMembershipId}", deleteDto.UserMembershipId);

            var userMembership = _userMembershipFactory.DeleteUserMembership(deleteDto);

            _userMembershipDomainService.EnsureCanDelete(userMembership);

            _logger.LogInformation("Orchestrator: UserMembership {UserMembershipId} đã được xoá thành công", deleteDto.UserMembershipId);
        }

        private async Task<ResultDTO<WalletHttpResponseDTO>> GetWalletByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu GetWalletByUserIdAsync với userId = {UserId}", userId);
            var createWallet = await _walletServiceClient.GetWalletByUserIdAsync(userId, cancellationToken);
            if (createWallet?.Data == null)
            {
                _logger.LogError("Orchestrator: Không thể kết nối đến WalletService để tạo ví cho userId = {UserId}", userId);
                throw new ConnectServiceFailedException("WalletService");
            }

            if (!createWallet.Data.Status)
            {
                _logger.LogError("Orchestrator: không lấy ví không thành công cho userId = {UserId} với lý do: {Msg}", userId, createWallet.Data.Msg);
                throw new HttpResposeFailedException(createWallet.Data.Msg, createWallet.StatusCode);
            }
            return createWallet.Data;
        }

        public async Task<ResultDTO> CreateWalletTransactionAsync(CreateWalletTransactionRequestDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateWalletTransactionAsync với userId = {UserId}", request.WalletId);   
            var createWallet = await _walletServiceClient.CreateWalletTransactionAsync(request, cancellationToken);
            if (createWallet?.Data == null)
            {
                _logger.LogError("Orchestrator: Không thể kết nối đến WalletService để tạo ví cho userId = {UserId}", request.WalletId);
                throw new ConnectServiceFailedException("WalletService");
            }

            if (!createWallet.Data.Status)
            {
                _logger.LogError("Orchestrator: không tạo giao dịch ví không thành công cho walletId = {WalletId} với lý do: {Msg}", request.WalletId, createWallet.Data.Msg);
                throw new HttpResposeFailedException(createWallet.Data.Msg, createWallet.StatusCode);
            }
            return createWallet.Data;
        }
    }
}
