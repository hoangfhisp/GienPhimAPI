using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.UserService.Application.DTOs.Client.User
{
    public class ListUserRequestDTO
    {
        [Required(ErrorMessage = "Trường 'page' là bắt buộc.")]
        public int page { get; set; } = 1;
        [Required(ErrorMessage = "Trường 'limit' là bắt buộc.")]
        public int limit { get; set; } = 10;
    }
}
