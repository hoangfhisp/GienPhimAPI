using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Invalids
{
    public class InvalidPhanLoaiTheLoaiException : BusinessException
    {
        public override int StatusCode => 400;

        public InvalidPhanLoaiTheLoaiException(string reason)
            : base($"Dữ liệu PhanLoaiTheLoai không hợp lệ: {reason}") { }
    }
}
