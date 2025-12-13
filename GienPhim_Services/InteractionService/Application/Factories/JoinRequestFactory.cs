using GienPhim_Services.InteractionService.Application.DTOs.Client.JoinRequest;
using GienPhim_Services.InteractionService.Application.Interfaces.Factories;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Factories
{
    public class JoinRequestFactory : IJoinRequestFactory
    {
        public JoinRequest CreateJoinRequest(JoinRequestCreateDTO dto, string currentUserId, DateTime nowUtc)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.GroupId == Guid.Empty)
                throw new ArgumentException("GroupId không được để trống.", nameof(dto.GroupId));

            if (string.IsNullOrWhiteSpace(currentUserId))
                throw new ArgumentException("User hiện tại không hợp lệ.", nameof(currentUserId));

            return new JoinRequest
            {
                RequestId = Guid.NewGuid(),
                GroupId = dto.GroupId,
                RequestUserId = currentUserId,
                Status = "Pending",
                RequestedAt = nowUtc,
                ProcessedAt = null
            };
        }
    }
}
