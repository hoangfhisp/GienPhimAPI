namespace GienPhim_Services.InteractionService.Application.DTOs.Client.GroupMember
{
    public class GroupMemberListItemDTO
    {
        public string UserId { get; set; } = string.Empty;
        public bool IsOwner { get; set; }
        public bool IsMuted { get; set; }
    }
}
