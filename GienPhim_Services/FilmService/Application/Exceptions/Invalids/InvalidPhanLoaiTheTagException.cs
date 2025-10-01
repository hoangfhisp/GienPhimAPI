using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Invalids
{
    public class InvalidPhanLoaiTheTagException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidPhanLoaiTheTagException(string reason)
            : base($"Dữ liệu PhanLoaiTheTag không hợp lệ: {reason}") { }
    }
}
