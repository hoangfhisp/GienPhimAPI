using GienPhim_Services.FilmService.Shared.Exceptions;

namespace GienPhim_Services.FilmService.Application.Exceptions.Failed
{
    public class CreateDaoDienFailedException : BusinessException
    {
        public override int StatusCode => 400;  // Thường là 400 cho lỗi yêu cầu không hợp lệ

        // Lưu thông tin lỗi liên quan đến DaoDien khi tạo thất bại
        public string DaoDienName { get; }

        public CreateDaoDienFailedException(string daoDienName)
            : base($"Tạo DaoDien với tên {daoDienName} thất bại.")
        {
            DaoDienName = daoDienName;
        }
    }
}
