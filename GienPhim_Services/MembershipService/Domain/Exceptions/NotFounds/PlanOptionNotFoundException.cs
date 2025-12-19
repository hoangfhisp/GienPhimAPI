using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Domain.Exceptions.NotFounds
{
    public class PlanOptionNotFoundException : DomainException
    {
        public PlanOptionNotFoundException(string optionId)
            : base($"Không tìm thấy PlanOption với Id = {optionId}.") { }
    }
}
