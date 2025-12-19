using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Repositories
{
    public interface IUserMembershipRepository
    {
        Task<(List<UserMembership> userMemberships, int total, int last)> GetAllUserMembershipsAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<UserMembership?> GetByIdAsync(string userMembershipId, CancellationToken ct = default);
        Task<List<UserMembership>> GetByUserIdAsync(string userId, CancellationToken ct = default);
        Task<UserMembership> CreateAsync(UserMembership userMembership, CancellationToken ct = default);
        Task<UserMembership> UpdateAsync(UserMembership userMembership, CancellationToken ct = default);
        Task DeleteAsync(UserMembership userMembership, CancellationToken ct = default);
        Task<UserMembership?> GetCurrentMembershipAsync(string userId, CancellationToken cancellationToken = default);
        Task<bool> HasActiveMembershipAsync(string userId, string optionId, CancellationToken cancellationToken = default);
        Task<List<UserMembership>> GetUserMemberByUserIdAndOptionId(string userId, string optionId, CancellationToken cancellationToken = default);
    }
}
