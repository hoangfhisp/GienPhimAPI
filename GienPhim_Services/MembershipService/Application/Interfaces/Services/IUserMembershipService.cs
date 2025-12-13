using GienPhim_Services.MembershipService.Application.DTOs.Client.UserMembership;
using GienPhim_Services.MembershipService.Application.DTOs.Common;


namespace GienPhim_Services.MembershipService.Application.Interfaces.Services
{
    public interface IUserMembershipService
    {
        Task<ResultListDTO<List<UserMembershipResponseDTO>>> GetAllUserMembershipsAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultDTO<UserMembershipResponseDTO>> GetDetailAsync(string userMembershipId, CancellationToken ct = default);
        Task<ResultDTO> CreateUserMembershipAsync(UserMembershipCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdateUserMembershipResponseDTO>> UpdateUserMembershipAsync(UserMembershipUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeleteUserMembershipAsync(UserMembershipDeleteDTO deleteDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UserMembershipResponseDTO>> GetCurrentMembershipAsync(string userId, CancellationToken cancellationToken = default);
        Task<ResultDTO> CheckUserHasMembershipAsync(string userId, string optionId, CancellationToken cancellationToken = default);
    }
}
