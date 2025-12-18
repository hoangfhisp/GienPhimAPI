using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Domain.Services.Interfaces
{
    public interface IFeedBackDomainService
    {
        void EnsureCanCreateFeedBack(Feedback fb);
        void EnsureCanUpdateFeedBack(Feedback fb);
    }
}
