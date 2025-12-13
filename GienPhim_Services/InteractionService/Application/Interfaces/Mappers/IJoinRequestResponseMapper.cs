using GienPhim_Services.InteractionService.Application.DTOs.Client.JoinRequest;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Mappers
{
    public interface IJoinRequestResponseMapper
    {
        JoinRequestResponseDTO ToResponseDto(JoinRequest request);

        List<JoinRequestResponseDTO> ToResponseDtoList(IEnumerable<JoinRequest> requests);
    }
}
