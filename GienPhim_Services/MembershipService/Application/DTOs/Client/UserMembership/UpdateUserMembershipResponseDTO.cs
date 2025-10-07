namespace GienPhim_Services.MembershipService.Application.DTOs.Client.UserMembership
{
    public class UpdateUserMembershipResponseDTO
    {
        public string UserMembershipId { get; set; } = string.Empty;
        public string OptionId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string TrangThai { get; set; } = string.Empty;
    }
}
