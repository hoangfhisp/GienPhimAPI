using GienPhim_Services.UserService.Application.DTOs.Common;

namespace GienPhim_Services.UserService.Application.DTOs.Admin.Role
{
    public class RoleResponseDTO
    {
        public bool Status { get; set; } // Trạng thái thành công hay thất bại
        public string Msg { get; set; } // Thông báo trả về cho người dùng
        public DataResponseRole Data { get; set; }
        public PaginationDTO Pagination { get; set; } // Thêm phân trang nếu cần
    }

    public class DataResponseRole
    {
        public string RoleId { get; set; }
        public string NameRole { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
