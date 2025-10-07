using GienPhim_Services.MembershipService.Application.DTOs.Client.MembershipPlan;
using GienPhim_Services.MembershipService.Application.Interfaces.Factories;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Factories
{
    public class MembershipPlanFactory : IMembershipPlanFactory
    {
        public MembershipPlan CreateMembershipPlan(MembershipPlanCreateDTO dto)
        {
            return new MembershipPlan
            {
                PlanId = Guid.NewGuid().ToString(), // Sinh ID tự động
                TenPlan = dto.TenPlan?.Trim() ?? string.Empty,
                PlanSlug = dto.PlanSlug?.Trim() ?? string.Empty,
                MoTaPlan = string.IsNullOrWhiteSpace(dto.MoTaPlan) ? null : dto.MoTaPlan.Trim(),
                PlanOptions = new List<PlanOption>(),
            };
        }

        public MembershipPlan UpdateMembershipPlan(MembershipPlanUpdateDTO dto)
        {
            return new MembershipPlan
            {
                PlanId = dto.PlanId.Trim(),
                TenPlan = dto.TenPlan?.Trim() ?? string.Empty,
                PlanSlug = dto.PlanSlug?.Trim() ?? string.Empty,
                MoTaPlan = string.IsNullOrWhiteSpace(dto.MoTaPlan) ? null : dto.MoTaPlan.Trim(),
                PlanOptions = new List<PlanOption>(),
            };
        }

        public MembershipPlan DeleteMembershipPlan(MembershipPlanDeleteDTO dto)
        {
            return new MembershipPlan
            {
                PlanId = dto.PlanId.Trim(),
            };
        }
    }
}
