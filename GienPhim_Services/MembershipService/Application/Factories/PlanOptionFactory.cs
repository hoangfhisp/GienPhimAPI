using GienPhim_Services.MembershipService.Application.DTOs.Client.PlanOption;
using GienPhim_Services.MembershipService.Application.Interfaces.Factories;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Factories
{
    public class PlanOptionFactory : IPlanOptionFactory
    {
        public PlanOption CreatePlanOption(PlanOptionCreateDTO dto)
        {
            return new PlanOption
            {
                OptionId = Guid.NewGuid().ToString(),
                PlanId = dto.PlanId.Trim(),
                ThoiGianSuDung = dto.ThoiGianSuDung,
                GiaOption = dto.GiaOption
            };
        }

        public PlanOption UpdatePlanOption(PlanOptionUpdateDTO dto)
        {
            return new PlanOption
            {
                OptionId = dto.OptionId.Trim(),
                PlanId = dto.PlanId.Trim(),
                ThoiGianSuDung = dto.ThoiGianSuDung,
                GiaOption = dto.GiaOption
            };
        }

        public PlanOption DeletePlanOption(PlanOptionDeleteDTO dto)
        {
            return new PlanOption
            {
                OptionId = dto.OptionId.Trim()
            };
        }
    }
}
