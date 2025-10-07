using GienPhim_Services.MembershipService.Application.DTOs.Client.UserMembership;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Factories
{
    public interface IUserMembershipFactory
    {
        UserMembership CreateUserMembership(UserMembershipCreateDTO dto);
        UserMembership UpdateUserMembership(UserMembershipUpdateDTO dto);
        UserMembership DeleteUserMembership(UserMembershipDeleteDTO dto);
    }
}
