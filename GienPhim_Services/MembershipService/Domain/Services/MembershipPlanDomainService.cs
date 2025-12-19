using GienPhim_Services.MembershipService.Domain.Entities;
using GienPhim_Services.MembershipService.Domain.Exceptions.Invalid;
using GienPhim_Services.MembershipService.Domain.Exceptions.NotFounds;
using GienPhim_Services.MembershipService.Domain.Services.Interfaces;

namespace GienPhim_Services.MembershipService.Domain.Services
{
    public class MembershipPlanDomainService : IMembershipPlanDomainService
    {
        public void EnsureCanCreate(MembershipPlan plan)
        {
            ValidateName(plan?.TenPlan);
            ValidateSlug(plan?.PlanSlug);
        }

        public void EnsureCanUpdate(MembershipPlan plan)
        {
            if (plan is null)
                throw new MembershipPlanNotFoundException("Không tìm thấy gói thành viên để cập nhật.");

            ValidateName(plan.TenPlan);
            ValidateSlug(plan.PlanSlug);
        }

        public void EnsureCanDelete(MembershipPlan plan)
        {
            if (plan is null)
                throw new MembershipPlanNotFoundException("Không tìm thấy gói thành viên để xoá.");
        }

        private static void ValidateName(string? name)
        {
            var v = (name ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(v))
                throw new InvalidMembershipPlanException("Tên gói thành viên không được để trống.");
            if (v.Length > 200)
                throw new InvalidMembershipPlanException("Tên gói thành viên vượt quá 200 ký tự.");
        }

        private static void ValidateSlug(string? slug)
        {
            var v = (slug ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(v))
                throw new InvalidMembershipPlanException("Slug của gói thành viên không được để trống.");
            if (v.Length > 200)
                throw new InvalidMembershipPlanException("Slug vượt quá 200 ký tự.");
        }
    }
}
