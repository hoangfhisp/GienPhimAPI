using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Domain.Services.Interfaces
{
    public interface IUserMembershipDomainService
    {
        void EnsureCanCreate(UserMembership userMembership);
        void EnsureCanUpdate(UserMembership userMembership);
        void EnsureCanDelete(UserMembership userMembership);
    }
}
