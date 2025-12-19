namespace GienPhim_Services.InteractionService.Application.DTOs.Client.ChatMessage
{
    public class ChatMessageResponseDTO
    {
        public Guid MessageId { get; set; }
        public Guid GroupId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public bool IsSystemMessage { get; set; }
        public string? LoaiTinNhan { get; set; }
    }
}
