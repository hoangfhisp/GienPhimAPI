using GienPhim_Services.MembershipService.Application.DTOs.Client.MembershipPlan;
using GienPhim_Services.MembershipService.Application.Interfaces.Mappers;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Mappers
{
    public class MembershipPlanResponseMapper : IMembershipPlanResponseMapper
    {
        // Chuyển entity → DTO cơ bản (dùng cho GetAll, GetById)
        public MembershipPlanResponseDTO ToDto(MembershipPlan plan)
        {
            return new MembershipPlanResponseDTO
            {
                PlanId = plan.PlanId,
                TenPlan = plan.TenPlan,
                PlanSlug = plan.PlanSlug,
                MoTaPlan = plan.MoTaPlan
            };
        }

        // Danh sách entity → danh sách DTO
        public List<MembershipPlanResponseDTO> ToDtoList(List<MembershipPlan> plans)
        {
            return plans.Select(p => ToDto(p)).ToList();
        }

        // Chuyển entity → DTO sau update (dùng cho PUT / PATCH)
        public UpdateMembershipPlanResponseDTO ToUpdateDto(MembershipPlan plan)
        {
            return new UpdateMembershipPlanResponseDTO
            {
                PlanId = plan.PlanId,
                TenPlan = plan.TenPlan,
                PlanSlug = plan.PlanSlug,
                MoTaPlan = plan.MoTaPlan,
            };
        }

        // Danh sách entity → danh sách DTO update
        public List<UpdateMembershipPlanResponseDTO> ToUpdateDtoList(List<MembershipPlan> plans)
        {
            return plans.Select(p => ToUpdateDto(p)).ToList();
        }
    }
}
