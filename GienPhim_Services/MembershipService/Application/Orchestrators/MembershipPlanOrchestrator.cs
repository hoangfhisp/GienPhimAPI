using GienPhim_Services.MembershipService.Application.DTOs.Client.MembershipPlan;
using GienPhim_Services.MembershipService.Application.Interfaces.Factories;
using GienPhim_Services.MembershipService.Application.Interfaces.Orchestrators;
using GienPhim_Services.MembershipService.Domain.Entities;
using GienPhim_Services.MembershipService.Domain.Services.Interfaces;

namespace GienPhim_Services.MembershipService.Application.Orchestrators
{
    public class MembershipPlanOrchestrator : IMembershipPlanOrchestrator
    {
        private readonly IMembershipPlanFactory _membershipPlanFactory;
        private readonly IMembershipPlanDomainService _membershipPlanDomainService;
        private readonly ILogger<MembershipPlanOrchestrator> _logger;

        public MembershipPlanOrchestrator(
            IMembershipPlanFactory membershipPlanFactory,
            IMembershipPlanDomainService membershipPlanDomainService,
            ILogger<MembershipPlanOrchestrator> logger)
        {
            _membershipPlanFactory = membershipPlanFactory;
            _membershipPlanDomainService = membershipPlanDomainService;
            _logger = logger;
        }

        // 🧩 Tạo MembershipPlan mới
        public async Task<MembershipPlan> CreateMembershipPlanAsync(MembershipPlanCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateMembershipPlanAsync với tên gói {TenPlan}", createDto.TenPlan);

            // Factory: khởi tạo entity từ DTO
            var plan = _membershipPlanFactory.CreateMembershipPlan(createDto);

            // DomainService: xác thực logic nghiệp vụ nội tại
            _membershipPlanDomainService.EnsureCanCreate(plan);

            _logger.LogInformation("Orchestrator: Gói thành viên {TenPlan} đã được validate và khởi tạo thành công", createDto.TenPlan);

            return plan;
        }

        // 🧩 Cập nhật MembershipPlan
        public async Task<MembershipPlan> UpdateMembershipPlanAsync(MembershipPlanUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateMembershipPlanAsync với Id={PlanId}", updateDto.PlanId);

            var plan = _membershipPlanFactory.UpdateMembershipPlan(updateDto);

            _membershipPlanDomainService.EnsureCanUpdate(plan);

            _logger.LogInformation("Orchestrator: Gói thành viên Id={PlanId} đã được cập nhật thành công", updateDto.PlanId);

            return plan;
        }

        // 🧩 Xoá MembershipPlan
        public async Task DeleteMembershipPlanAsync(MembershipPlanDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteMembershipPlanAsync với Id={PlanId}", deleteDto.PlanId);

            var plan = _membershipPlanFactory.DeleteMembershipPlan(deleteDto);

            _membershipPlanDomainService.EnsureCanDelete(plan);

            _logger.LogInformation("Orchestrator: Gói thành viên Id={PlanId} đã được xoá thành công", deleteDto.PlanId);
        }
    }
}
