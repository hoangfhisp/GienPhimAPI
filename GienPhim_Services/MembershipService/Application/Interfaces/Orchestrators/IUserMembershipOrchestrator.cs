using GienPhim_Services.MembershipService.Application.DTOs.Client.UserMembership;
using GienPhim_Services.MembershipService.Application.DTOs.Common;
using GienPhim_Services.MembershipService.Application.DTOs.Wallets;
using GienPhim_Services.MembershipService.Domain.Entities;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Orchestrators
{
    public interface IUserMembershipOrchestrator
    {
        Task<(UserMembership userMembership, string walletId)> CreateUserMembershipAsync(UserMembershipCreateDTO createDto, decimal giaOption, int day, CancellationToken cancellationToken = default);
        Task<UserMembership> UpdateUserMembershipAsync(UserMembershipUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> CreateWalletTransactionAsync(CreateWalletTransactionRequestDTO request, CancellationToken cancellationToken = default);
        Task DeleteUserMembershipAsync(UserMembershipDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
