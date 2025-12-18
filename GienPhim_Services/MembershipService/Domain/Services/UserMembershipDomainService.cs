using GienPhim_Services.MembershipService.Domain.Entities;
using GienPhim_Services.MembershipService.Domain.Exceptions.Invalid;
using GienPhim_Services.MembershipService.Domain.Exceptions.NotFounds;
using GienPhim_Services.MembershipService.Domain.Services.Interfaces;

namespace GienPhim_Services.MembershipService.Domain.Services
{
    public class UserMembershipDomainService : IUserMembershipDomainService
    {
        public void EnsureCanCreate(UserMembership userMembership)
        {
            
            Validate(userMembership);
        }

        public void EnsureCanUpdate(UserMembership userMembership)
        {
            if (userMembership is null)
                throw new UserMembershipNotFoundException("Không tìm thấy UserMembership để cập nhật.");

            Validate(userMembership);
        }

        public void EnsureCanDelete(UserMembership userMembership)
        {
            if (userMembership is null)
                throw new UserMembershipNotFoundException("Không tìm thấy UserMembership để xóa.");
        }

        private static void Validate(UserMembership userMembership)
        {
            if (userMembership is null)
                throw new InvalidUserMembershipException("Đối tượng UserMembership không được null.");

            if (string.IsNullOrWhiteSpace(userMembership.UserId))
                throw new InvalidUserMembershipException("UserId không được để trống.");

            if (string.IsNullOrWhiteSpace(userMembership.OptionId))
                throw new InvalidUserMembershipException("OptionId không được để trống.");

            if (userMembership.NgayBatDau >= userMembership.NgayKetThuc)
                throw new InvalidUserMembershipException("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");

            if (string.IsNullOrWhiteSpace(userMembership.TrangThai))
                throw new InvalidUserMembershipException("Trạng thái gói thành viên không được để trống.");
        }
    }
}
