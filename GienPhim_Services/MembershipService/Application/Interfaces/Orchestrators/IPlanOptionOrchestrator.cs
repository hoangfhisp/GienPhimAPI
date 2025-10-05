using GienPhim_Services.MembershipService.Application.DTOs.Client.PlanOption;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Orchestrators
{
    public interface IPlanOptionOrchestrator
    {
        Task<PlanOption> CreatePlanOptionAsync(PlanOptionCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<PlanOption> UpdatePlanOptionAsync(PlanOptionUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task DeletePlanOptionAsync(PlanOptionDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
