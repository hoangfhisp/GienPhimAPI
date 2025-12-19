using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.AuthService.Application.DTOs.User
{
    public class ChangeEmailHttpRequestDTO
    {
        [Required(ErrorMessage = "UserId bắt buộc phải nhập!")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "NewEmail bắt buộc phải nhập!")]
        [EmailAddress(ErrorMessage = "NewEmail không đúng định dạng!")]
        public string NewEmail { get; set; }
    }
}
