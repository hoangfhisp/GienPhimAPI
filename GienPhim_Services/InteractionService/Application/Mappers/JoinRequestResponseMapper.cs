using GienPhim_Services.InteractionService.Application.DTOs.Client.JoinRequest;
using GienPhim_Services.InteractionService.Application.Interfaces.Mappers;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Mappers
{
    public class JoinRequestResponseMapper : IJoinRequestResponseMapper
    {
        public JoinRequestResponseDTO ToResponseDto(JoinRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return new JoinRequestResponseDTO
            {
                RequestId = request.RequestId,
                GroupId = request.GroupId,
                RequestUserId = request.RequestUserId,
                Status = request.Status,
                RequestedAt = request.RequestedAt,
                ProcessedAt = request.ProcessedAt
            };
        }

        public List<JoinRequestResponseDTO> ToResponseDtoList(IEnumerable<JoinRequest> requests)
        {
            return requests.Select(ToResponseDto).ToList();
        }
    }
}
