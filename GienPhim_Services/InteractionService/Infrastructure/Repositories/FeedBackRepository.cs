using GienPhim_Services.InteractionService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GienPhim_Services.InteractionService.Infrastructure.Repositories
{
    public class FeedBackRepository : IFeedBackRepository
    {
        private readonly InteractionDbContext _context;
        public FeedBackRepository(InteractionDbContext context)
        {
            _context = context;
        }

        // add Feedback
        public async Task<Feedback> AddFeedbackAsync(Feedback feedback, CancellationToken cancellationToken = default)
        {
            await _context.Feedback.AddAsync(feedback, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return feedback;
        }

        // get Feedback by Id
        public async Task<Feedback?> GetFeedbackByIdAsync(string feedbackId, CancellationToken cancellationToken = default)
        {
            return await _context.Feedback.FirstOrDefaultAsync(fb => fb.FeedbackId == feedbackId);
        }

        // get Feedback by UserId
        public async Task<Feedback?> GetFeedbackByUserIdAndPhimIdAsync(string userId,int phimId, CancellationToken cancellationToken = default)
        {
            return await _context.Feedback.FirstOrDefaultAsync(fb => fb.UserId == userId && fb.PhimId == phimId);
        }

        // update Feedback
        public async Task<Feedback> UpdateFeedbackAsync(Feedback feedback, CancellationToken cancellationToken = default)
        {
            _context.Feedback.Update(feedback);
            await _context.SaveChangesAsync(cancellationToken);
            return feedback;
        }

        // delete Feedback
        public async Task<Feedback> DeleteFeedbackAsync(Feedback feedback, CancellationToken cancellationToken = default)
        {
            _context.Feedback.Remove(feedback);
            await _context.SaveChangesAsync(cancellationToken);
            return feedback;
        }

    }
}
