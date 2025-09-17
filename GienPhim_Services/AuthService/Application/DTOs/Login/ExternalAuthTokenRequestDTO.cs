using System.ComponentModel.DataAnnotations;

namespace GienPhim_Services.AuthService.Application.DTOs.Login
{
    //client gửi IdToken để backend verify.
    public class ExternalAuthTokenRequestDTO
    {
        [Required(ErrorMessage = "Provider bắt buộc phải nhập")]
        public string Provider { get; set; }  // "Google" | "Facebook"

        [Required(ErrorMessage = "IdToken bắt buộc phải nhập")]
        public string IdToken { get; set; }
    }
}
