using GienPhim_Services.MembershipService.Domain.Entities;
using GienPhim_Services.MembershipService.Domain.Exceptions.Invalid;
using GienPhim_Services.MembershipService.Domain.Exceptions.NotFounds;
using GienPhim_Services.MembershipService.Domain.Services.Interfaces;

namespace GienPhim_Services.MembershipService.Domain.Services
{
    public class PlanOptionDomainService : IPlanOptionDomainService
    {
        public void EnsureCanCreate(PlanOption option)
        {
            Validate(option);
        }

        public void EnsureCanUpdate(PlanOption option)
        {
            if (option is null)
                throw new PlanOptionNotFoundException("Không tìm thấy PlanOption để cập nhật.");

            Validate(option);
        }

        public void EnsureCanDelete(PlanOption option)
        {
            if (option is null)
                throw new PlanOptionNotFoundException("Không tìm thấy PlanOption để xóa.");
        }

        private static void Validate(PlanOption option)
        {
            if (option is null)
                throw new InvalidPlanOptionException("Đối tượng PlanOption không được null.");

            if (option.ThoiGianSuDung <= 0)
                throw new InvalidPlanOptionException("Thời gian sử dụng phải lớn hơn 0.");

            if (option.GiaOption < 0)
                throw new InvalidPlanOptionException("Giá gói thành viên không hợp lệ.");
        }
    }
}
