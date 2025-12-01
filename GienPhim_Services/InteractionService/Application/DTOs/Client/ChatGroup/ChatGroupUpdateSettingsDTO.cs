using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.InteractionService.Application.DTOs.Client.ChatGroup
{
    public class ChatGroupUpdateSettingsDTO
    {
        [Required(ErrorMessage = "Trạng thái cho phép chat là bắt buộc.")]
        public bool AllowChat { get; set; }

        [Required(ErrorMessage = "Trạng thái cho phép mời là bắt buộc.")]
        public bool AllowInvite { get; set; }

        [Required(ErrorMessage = "Trạng thái cho phép gia nhập bằng link là bắt buộc.")]
        public bool AllowJoinByLink { get; set; }
    }
}
