using GienPhim_Services.MembershipService.Application.DTOs.Client.MembershipPlan;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Mappers
{
    public interface IMembershipPlanResponseMapper
    {
        MembershipPlanResponseDTO ToDto(MembershipPlan plan);
        List<MembershipPlanResponseDTO> ToDtoList(List<MembershipPlan> plans);
        UpdateMembershipPlanResponseDTO ToUpdateDto(MembershipPlan plan);
        List<UpdateMembershipPlanResponseDTO> ToUpdateDtoList(List<MembershipPlan> plans);
    }
}
