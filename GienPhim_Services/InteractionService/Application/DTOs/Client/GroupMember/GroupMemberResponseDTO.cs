namespace GienPhim_Services.InteractionService.Application.DTOs.Client.GroupMember
{
    public class GroupMemberResponseDTO
    {
        public Guid GroupMemberId { get; set; }
        public Guid GroupId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public bool IsOwner { get; set; }
        public bool CanChat { get; set; }
        public bool CanControlFilm { get; set; }
        public bool IsMuted { get; set; }

        public DateTime JoinedAt { get; set; }
    }

}
