using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Domain.Services.Interfaces
{
    public interface IPlanOptionDomainService
    {
        void EnsureCanCreate(PlanOption option);
        void EnsureCanUpdate(PlanOption option);
        void EnsureCanDelete(PlanOption option);
    }
}
