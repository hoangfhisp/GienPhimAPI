using GienPhim_Services.MembershipService.Application.DTOs.Client.MembershipPlan;
using GienPhim_Services.MembershipService.Application.DTOs.Common;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Services
{
    public interface IMembershipPlanService
    {
        Task<ResultListDTO<List<MembershipPlanResponseDTO>>> GetAllMembershipPlansAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultDTO<MembershipPlanResponseDTO>> GetDetailAsync(string planId, CancellationToken cancellationToken = default);
        Task<ResultDTO> CreateMembershipPlanAsync(MembershipPlanCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdateMembershipPlanResponseDTO>> UpdateMembershipPlanAsync(MembershipPlanUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeleteMembershipPlanAsync(MembershipPlanDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
