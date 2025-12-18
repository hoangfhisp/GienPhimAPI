using GienPhim_Services.InteractionService.Application.DTOs.Client.FeedBack;
using GienPhim_Services.InteractionService.Application.DTOs.Client.Http;
using GienPhim_Services.InteractionService.Application.DTOs.Client.Phim;
using GienPhim_Services.InteractionService.Application.DTOs.Common;
using GienPhim_Services.InteractionService.Application.DTOs.User;
using GienPhim_Services.InteractionService.Application.Exceptions.Failed;
using GienPhim_Services.InteractionService.Application.Exceptions.NotFounds;
using GienPhim_Services.InteractionService.Application.Interfaces.Factories;
using GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators;
using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Domain.Services.Interfaces;
using GienPhim_Services.InteractionService.Infrastructure.Data.HttpClients.Interfaces;

namespace GienPhim_Services.InteractionService.Application.Orchestrators
{
    public class FeedBackOrchestrator : IFeedBackOrchestrator
    {
        private readonly IFeedBackDomainService _feedBackDomainService;
        private readonly IFeedBackFactory _feedBackFactory;
        private readonly IUserServiceClient _userServiceClient;
        private readonly IPhimServiceClient _phimServiceClient;
        private readonly ILogger<FeedBackOrchestrator> _logger;

        public FeedBackOrchestrator(IFeedBackDomainService feedBackDomainService, IFeedBackFactory feedBackFactory
            ,IUserServiceClient userServiceClient , IPhimServiceClient phimServiceClient
            , ILogger<FeedBackOrchestrator> logger)
        {
            _feedBackDomainService = feedBackDomainService;
            _feedBackFactory = feedBackFactory;
            _userServiceClient = userServiceClient;
            _phimServiceClient = phimServiceClient;
            _logger = logger;
        }

        public async Task<Feedback> ProcessingLikeDislikeAsync(ProcessingFeedBackLikeDislikeRequestDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("InteractionService: Bắt đầu xử lý trong Orchestrator cho like, dislike phimId: {PhimId} và userId: {UserId}", request.PhimId, request.UserId);
            var feedback = _feedBackFactory.CreateLikeDislike(request);
            // Kiểm tra userId có tồn tại không
            _feedBackDomainService.EnsureCanCreateFeedBack(feedback);
            await FindUserByIdAsync(request.UserId, cancellationToken);
            await FindPhimByPhimIdAsync(request.PhimId, cancellationToken);

            _logger.LogInformation("InteractionService: Kết thúc xử lý trong Orchestrator cho like, dislike phimId: {PhimId} và userId: {UserId}", request.PhimId, request.UserId);
            return feedback;
        }

        //private methods
        // Tìm user theo id bằng UserService
        public async Task<ResultDTO<UserHttpResponseDTO>?> FindUserByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu FindUserById với userId = {UserId}", userId);
            var user = await _userServiceClient.FindUserByUserIdAsync(userId, cancellationToken);

            // Không kết nối được đến UserService
            if (user == null)
            {
                _logger.LogError("Orchestrator: FindUserById không thể kết nối đến UserService với userId = {UserId}", userId);
                throw new ConnectServiceFailedException("UserService");
            }

            // Không tìm thấy user
            if (!user.Data.Status)
            {
                _logger.LogWarning("Orchestrator: FindUserById không tìm thấy userId = {UserId}, Lỗi: {Error}", userId, user.Data.Msg);
                throw new UserNotFoundException(user.Data.Msg, user.StatusCode);
            }

            _logger.LogInformation("Orchestrator: FindUserById tìm thấy userId = {UserId}", userId);
            return user.Data;
        }

        // Tìm phim theo phimId bằng PhimService
        public async Task<ResultDTO<PhimHttpResponseDTO>?> FindPhimByPhimIdAsync(int phimId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu FindPhimByPhimId với phimId = {PhimId}", phimId);
            var phim = await _phimServiceClient.FindPhimByPhimIdAsync(phimId, cancellationToken);

            // Không kết nối được đến UserService
            if (phim == null)
            {
               _logger.LogError("Orchestrator: FindPhimByPhimIdAsync không thể kết nối đến PhimService với phimId = {PhimId}", phimId);
                throw new ConnectServiceFailedException("PhimService");
            }

            // Không tìm thấy user
            if (!phim.Data.Status)
            {
                _logger.LogWarning("Orchestrator: FindPhimByPhimIdAsync không tìm thấy phimId = {PhimId}, Lỗi: {Error}", phimId, phim.Data.Msg);
                throw new PhimNotFoundException(phim.Data.Msg, phim.StatusCode);
            }

            _logger.LogInformation("Orchestrator: FindPhimByPhimIdAsync tìm thấy phimId = {PhimId}", phimId);
            return phim.Data;
        }

        // Cập nhật like dislike cho phim bằng PhimService
        public async Task<ResultDTO> UpdatePhimLikeDisLikeAsync(UpdatePhimLikeDisLikeRequestDTO request, CancellationToken cancellationToken = default)
        {
            var user = await _phimServiceClient.UpdatePhimLikeDisLikeAsync(request, cancellationToken);

            // Không kết nối được đến UserService
            if (user == null)
            {
                _logger.LogError("Orchestrator: UpdatePhimLikeDisLikeAsync không thể kết nối đến PhimService với phimId = {PhimId}", request.PhimId);
                throw new ConnectServiceFailedException("PhimService");
            }

            // Không tìm thấy user
            if (!user.Data.Status)
            {
                _logger.LogWarning("Orchestrator: UpdatePhimLikeDisLikeAsync không tìm thấy phimId = {PhimId}, Lỗi: {Error}", request.PhimId, user.Data.Msg);
                throw new PhimNotFoundException(user.Data.Msg, user.StatusCode);
            }

            _logger.LogInformation("Orchestrator: UpdatePhimLikeDisLikeAsync cập nhật thành công phimId = {PhimId}", request.PhimId);
            return user.Data;
        }
    }
}
