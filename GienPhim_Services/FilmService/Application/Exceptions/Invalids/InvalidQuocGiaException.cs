using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Invalids
{
    public class InvalidQuocGiaException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidQuocGiaException(string reason)
            : base($"Dữ liệu quốc gia không hợp lệ: {reason}") { }
    }
}
