using GienPhim_Services.InteractionService.Application.DTOs.Client.FeedBack;
using GienPhim_Services.InteractionService.Application.DTOs.Client.Phim;
using GienPhim_Services.InteractionService.Application.DTOs.Common;
using GienPhim_Services.InteractionService.Application.Exceptions.Failed;
using GienPhim_Services.InteractionService.Application.Exceptions.NotFounds;
using GienPhim_Services.InteractionService.Application.Interfaces.Mappers;
using GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators;
using GienPhim_Services.InteractionService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Application.Interfaces.Services;
using System.ComponentModel;

namespace GienPhim_Services.InteractionService.Application.Services
{
    public class FeedBackService : IFeedBackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFeedBackOrchestrator _feedBackOrchestrator;
        private readonly IFeedBackResponseMapper _feedBackResponseMapper;
        private readonly ILogger<FeedBackService> _logger;

        public FeedBackService(IUnitOfWork unitOfWork, IFeedBackOrchestrator feedBackOrchestrator, IFeedBackResponseMapper feedBackResponseMapper, ILogger<FeedBackService> logger)
        {
            _unitOfWork = unitOfWork;
            _feedBackOrchestrator = feedBackOrchestrator;
            _feedBackResponseMapper = feedBackResponseMapper;
            _logger = logger;
        }

        // Lấy Like Dislike 
        public async Task<ResultDTO<FeedBackLikeDislikeResponseDTO>> GetLikeDislikeByUserIdAndPhimIdAsync(GetFeedBackLikeDislikeRequestDTO request , CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("InteractionService: Bắt đầu lấy dữ liệu like, dislike cho phimId: {PhimId} và userId: {UserId}", request.PhimId, request.UserId);
            var feedback = await _unitOfWork.FeedBacks.GetFeedbackByUserIdAndPhimIdAsync(request.UserId, request.PhimId);
            if(feedback == null)
            {
                _logger.LogWarning("InteractionService: Không tìm thấy dữ liệu like, dislike cho phimId: {PhimId} và userId: {UserId}", request.PhimId, request.UserId);
                throw new FeedBackLikeDislikeNotFoundException();
            }

            var responseMapper = _feedBackResponseMapper.toLikeDislikeDto(feedback);
            _logger.LogInformation("InteractionService: Lấy dữ liệu like, dislike thành công cho phimId: {PhimId} và userId: {UserId}", request.PhimId, request.UserId);
            return ResultDTO<FeedBackLikeDislikeResponseDTO>.Success(responseMapper,"Lấy dữ liệu like, dislike thành công");
        }

        // Xử lý Like Dislike
        public async Task<ResultDTO> ProcessingLikeDislikeAsync(ProcessingFeedBackLikeDislikeRequestDTO request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("InteractionService: Bắt đầu xử lý like, dislike cho phimId: {PhimId} và userId: {UserId}", request.PhimId, request.UserId);
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var feedback = await _feedBackOrchestrator.ProcessingLikeDislikeAsync(request, cancellationToken);
            var existingFeedback = await _unitOfWork.FeedBacks.GetFeedbackByUserIdAndPhimIdAsync(request.UserId, request.PhimId);
            if (existingFeedback == null)
            {
                var add = await _unitOfWork.FeedBacks.AddFeedbackAsync(feedback, cancellationToken);
                if(add == null)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    _logger.LogError("InteractionService: Thêm mới like, dislike thất bại cho phimId: {PhimId} và userId: {UserId}", request.PhimId, request.UserId);
                    throw new CreateFeedBackLikeDislikeFailedException();
                }

                var requestUpdatePhims = new UpdatePhimLikeDisLikeRequestDTO
                {
                    PhimId = request.PhimId,
                    IsLike = request.LuotThichP,
                    IsDislike = request.LuotDislikeP,
                    OldIsLike = null,
                    OldIsDislike = null
                };

                await _feedBackOrchestrator.UpdatePhimLikeDisLikeAsync(requestUpdatePhims, cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                _logger.LogInformation("InteractionService: Xử lý like, dislike thành công cho phimId: {PhimId} và userId: {UserId}", request.PhimId, request.UserId);
                return ResultDTO.Success("Xử lý like, dislike thành công");
            }
            bool? oldLike = existingFeedback?.LuotThichP;
            bool? oldDislike = existingFeedback?.LuotDislikeP;

            existingFeedback.LuotThichP = request.LuotThichP; // nếu like là true thì like, ngược lại là false
            existingFeedback.LuotDislikeP = request.LuotDislikeP;
            existingFeedback.ThoiGianCapNhat = DateTime.UtcNow;

            var update = await _unitOfWork.FeedBacks.UpdateFeedbackAsync(existingFeedback, cancellationToken);
            if (update == null)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError("InteractionService: Cập nhật like, dislike thất bại cho phimId: {PhimId} và userId: {UserId}", request.PhimId, request.UserId);
                throw new UpdateFeedBackLikeDislikeFailedException();
            }

            var requestUpdatePhim = new UpdatePhimLikeDisLikeRequestDTO
            {
                PhimId = request.PhimId,
                IsLike = request.LuotThichP,
                IsDislike = request.LuotDislikeP,
                OldIsLike = oldLike,
                OldIsDislike = oldDislike
            };
            await _feedBackOrchestrator.UpdatePhimLikeDisLikeAsync(requestUpdatePhim, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("InteractionService: Xử lý like, dislike thành công cho phimId: {PhimId} và userId: {UserId}", request.PhimId, request.UserId);
            return ResultDTO.Success("Xử lý like, dislike thành công");
        }

    }
}
