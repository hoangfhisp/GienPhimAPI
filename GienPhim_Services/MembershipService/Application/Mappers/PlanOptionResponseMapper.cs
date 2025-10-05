using GienPhim_Services.MembershipService.Application.DTOs.Client.PlanOption;
using GienPhim_Services.MembershipService.Application.Interfaces.Mappers;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Mappers
{
    public class PlanOptionResponseMapper : IPlanOptionResponseMapper
    {
        public PlanOptionResponseDTO ToDto(PlanOption option)
        {
            return new PlanOptionResponseDTO
            {
                OptionId = option.OptionId,
                PlanId = option.PlanId,
                ThoiGianSuDung = option.ThoiGianSuDung,
                GiaOption = option.GiaOption
            };
        }

        public List<PlanOptionResponseDTO> ToDtoList(List<PlanOption> options)
        {
            return options.Select(ToDto).ToList();
        }

        public UpdatePlanOptionResponseDTO ToUpdateDto(PlanOption option)
        {
            return new UpdatePlanOptionResponseDTO
            {
                OptionId = option.OptionId,
                PlanId = option.PlanId,
                ThoiGianSuDung = option.ThoiGianSuDung,
                GiaOption = option.GiaOption
            };
        }

        public List<UpdatePlanOptionResponseDTO> ToUpdateDtoList(List<PlanOption> options)
        {
            return options.Select(ToUpdateDto).ToList();
        }
    }
}