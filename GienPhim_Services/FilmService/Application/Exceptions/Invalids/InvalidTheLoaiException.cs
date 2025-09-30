using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Invalids
{
    public class InvalidTheLoaiException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidTheLoaiException(string reason)
            : base($"Dữ liệu thể loại không hợp lệ: {reason}") { }
    }
}
