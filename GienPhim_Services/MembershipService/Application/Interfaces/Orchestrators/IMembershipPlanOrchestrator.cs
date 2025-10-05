using GienPhim_Services.MembershipService.Application.DTOs.Client.MembershipPlan;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Orchestrators
{
    public interface IMembershipPlanOrchestrator
    {
        Task<MembershipPlan> CreateMembershipPlanAsync(MembershipPlanCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<MembershipPlan> UpdateMembershipPlanAsync(MembershipPlanUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task DeleteMembershipPlanAsync(MembershipPlanDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
