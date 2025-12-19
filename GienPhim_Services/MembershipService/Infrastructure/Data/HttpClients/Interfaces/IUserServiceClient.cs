using GienPhim_Services.MembershipService.Application.DTOs.Common;
using GienPhim_Services.MembershipService.Application.DTOs.Client.User;

namespace GienPhim_Services.MembershipService.Infrastructure.Data.HttpClients.Interfaces
{
    public interface IUserServiceClient
    {
        Task<ApiResponse<ResultDTO<UserHttpResponseDTO>>?> FindUserByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    }
}
