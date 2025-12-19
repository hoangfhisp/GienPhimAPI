namespace GienPhim_Services.InteractionService.Application.DTOs.Client.GroupMember
{
    public class GroupMemberUpdatePermissionsDTO
    {
        public bool? IsMuted { get; set; }
        public bool? CanChat { get; set; }
        public bool? CanControlFilm { get; set; }
    }

}
