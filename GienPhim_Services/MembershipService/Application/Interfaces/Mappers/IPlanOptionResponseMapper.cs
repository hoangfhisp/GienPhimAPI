using GienPhim_Services.MembershipService.Application.DTOs.Client.PlanOption;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Mappers
{
    public interface IPlanOptionResponseMapper
    {
        PlanOptionResponseDTO ToDto(PlanOption option);
        List<PlanOptionResponseDTO> ToDtoList(List<PlanOption> options);
        UpdatePlanOptionResponseDTO ToUpdateDto(PlanOption option);
        List<UpdatePlanOptionResponseDTO> ToUpdateDtoList(List<PlanOption> options);
    }
}
