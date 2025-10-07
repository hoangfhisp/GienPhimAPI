using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Domain.Services.Interfaces
{
    public interface IMembershipPlanDomainService
    {
        void EnsureCanCreate(MembershipPlan plan);
        void EnsureCanUpdate(MembershipPlan plan);
        void EnsureCanDelete(MembershipPlan plan);
    }
}
