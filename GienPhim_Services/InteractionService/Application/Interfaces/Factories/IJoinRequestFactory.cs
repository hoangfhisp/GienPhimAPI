using GienPhim_Services.InteractionService.Application.DTOs.Client.JoinRequest;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Factories
{
    public interface IJoinRequestFactory
    {
        JoinRequest CreateJoinRequest(JoinRequestCreateDTO dto, string currentUserId, DateTime nowUtc);
    }
}
