using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Invalids
{
    public class InvalidDienVienException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidDienVienException(string reason)
            : base($"Dữ liệu diễn viên không hợp lệ: {reason}") { }
    }
}
