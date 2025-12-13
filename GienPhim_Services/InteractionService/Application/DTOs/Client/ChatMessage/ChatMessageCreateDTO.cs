namespace GienPhim_Services.InteractionService.Application.DTOs.Client.ChatMessage
{
    public class ChatMessageCreateDTO
    {
        public Guid GroupId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? LoaiTinNhan { get; set; } = "text";
    }
}
