using GienPhim_Services.MembershipService.Application.DTOs.Client.PlanOption;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Factories
{
    public interface IPlanOptionFactory
    {
        PlanOption CreatePlanOption(PlanOptionCreateDTO dto);
        PlanOption UpdatePlanOption(PlanOptionUpdateDTO dto);
        PlanOption DeletePlanOption(PlanOptionDeleteDTO dto);
    }
}
