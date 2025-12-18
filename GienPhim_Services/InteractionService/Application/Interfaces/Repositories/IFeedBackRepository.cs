using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Repositories
{
    public interface IFeedBackRepository
    {
        Task<Feedback> AddFeedbackAsync(Feedback feedback, CancellationToken cancellationToken = default); // add Feedback
        Task<Feedback?> GetFeedbackByIdAsync(string feedbackId, CancellationToken cancellationToken = default); // get Feedback by Id
        Task<Feedback?> GetFeedbackByUserIdAndPhimIdAsync(string userId, int phimId, CancellationToken cancellationToken = default); // get Feedback by UserId
        Task<Feedback> UpdateFeedbackAsync(Feedback feedback, CancellationToken cancellationToken = default); // update Feedback
        Task<Feedback> DeleteFeedbackAsync(Feedback feedback, CancellationToken cancellationToken = default); // delete Feedback
    }
}
