namespace GienPhim_Services.InteractionService.Application.DTOs.Client.JoinRequest
{
    public class JoinRequestResponseDTO
    {
        public Guid RequestId { get; set; }

        public Guid GroupId { get; set; }

        public string RequestUserId { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        public DateTime RequestedAt { get; set; }

        public DateTime? ProcessedAt { get; set; }
    }
}
