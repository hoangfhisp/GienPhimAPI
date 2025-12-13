using System.Net;

namespace GienPhim_Services.InteractionService.Application.DTOs.Common
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public T? Data { get; set; }
    }
}
