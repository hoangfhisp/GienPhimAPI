using GienPhim_Services.MembershipService.Application.DTOs.Client.MembershipPlan;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Factories
{
    public interface IMembershipPlanFactory
    {
        MembershipPlan CreateMembershipPlan(MembershipPlanCreateDTO dto);
        MembershipPlan UpdateMembershipPlan(MembershipPlanUpdateDTO dto);
        MembershipPlan DeleteMembershipPlan(MembershipPlanDeleteDTO dto);
    }
}
