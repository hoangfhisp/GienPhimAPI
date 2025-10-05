using GienPhim_Services.MembershipService.Shared.Exceptions;

namespace GienPhim_Services.MembershipService.Domain.Exceptions.NotFounds
{
    public class UserMembershipNotFoundException : DomainException
    {
        public UserMembershipNotFoundException(string id)
            : base($"Không tìm thấy UserMembership với Id = {id}.") { }
    }
}
