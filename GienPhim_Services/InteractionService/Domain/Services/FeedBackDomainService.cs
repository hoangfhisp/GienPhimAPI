using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Domain.Exceptions.Invalid;
using GienPhim_Services.InteractionService.Domain.Services.Interfaces;

namespace GienPhim_Services.InteractionService.Domain.Services
{
    public class FeedBackDomainService : IFeedBackDomainService
    {
        public void EnsureCanCreateFeedBack(Feedback fb)
        {
            if(fb is null)
                throw new InvalidFeedBackNullException();
            if (fb.UserId is null || fb.UserId.Trim() == "")
                throw new InvalidUserIdNullException();
            if (fb.PhimId <= 0)
                throw new InvalidPhimIdException();
            if(fb.LuotThichP && fb.LuotDislikeP)
                throw new InvalidLikeDislikeException();
        }

        public void EnsureCanUpdateFeedBack(Feedback fb)
        {
            if (fb is null)
                throw new InvalidFeedBackNullException();
            if (fb.UserId is null || fb.UserId.Trim() == "")
                throw new InvalidUserIdNullException();
            if (fb.PhimId <= 0)
                throw new InvalidPhimIdException();
            if (fb.LuotThichP && fb.LuotDislikeP)
                throw new InvalidLikeDislikeException();
        }
    }
}
