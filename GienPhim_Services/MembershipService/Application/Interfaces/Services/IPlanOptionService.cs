using GienPhim_Services.MembershipService.Application.DTOs.Client.PlanOption;
using GienPhim_Services.MembershipService.Application.DTOs.Common;

namespace GienPhim_Services.MembershipService.Application.Interfaces.Services
{
    public interface IPlanOptionService
    {
        Task<ResultListDTO<List<PlanOptionResponseDTO>>> GetAllPlanOptionsAsync(int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultListDTO<List<PlanOptionResponseDTO>>> GetPlanOptionsByPlanIdAnDayAsync(string plainId, int day, int page, int limit, CancellationToken cancellationToken = default);
        Task<ResultDTO<PlanOptionResponseDTO>> GetDetailAsync(string optionId, CancellationToken ct = default);
        Task<ResultDTO> CreatePlanOptionAsync(PlanOptionCreateDTO createDto, CancellationToken cancellationToken = default);
        Task<ResultDTO<UpdatePlanOptionResponseDTO>> UpdatePlanOptionAsync(PlanOptionUpdateDTO updateDto, CancellationToken cancellationToken = default);
        Task<ResultDTO> DeletePlanOptionAsync(PlanOptionDeleteDTO deleteDto, CancellationToken cancellationToken = default);
    }
}
