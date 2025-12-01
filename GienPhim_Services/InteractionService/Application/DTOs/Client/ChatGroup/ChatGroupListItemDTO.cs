namespace GienPhim_Services.InteractionService.Application.DTOs.Client.ChatGroup
{
    public class ChatGroupListItemDTO
    {
        public Guid GroupId { get; set; }

        public int PhimId { get; set; }
        public string PhimName { get; set; } = string.Empty;

        public string CreatedByUserId { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

        public DateTime ShowStartTime { get; set; }
        public DateTime ShowEndTime { get; set; }

        public bool AllowChat { get; set; }
        public bool AllowInvite { get; set; }
        public bool AllowJoinByLink { get; set; }

        public int MemberCount { get; set; }
    }
}
