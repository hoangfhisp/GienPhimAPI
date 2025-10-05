using GienPhim_Services.MembershipService.Application.DTOs.Client.UserMembership;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Orchestrators
{
    public interface IUserMembershipOrchestrator
    {
        Task<UserMembership> CreateUserMembershipAsync(UserMembershipCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<UserMembership> UpdateUserMembershipAsync(UserMembershipUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task DeleteUserMembershipAsync(UserMembershipDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
