using GienPhim_Services.MembershipService.Application.DTOs.Client.PlanOption;
using GienPhim_Services.MembershipService.Application.Interfaces.Factories;
using GienPhim_Services.MembershipService.Application.Interfaces.Orchestrators;
using GienPhim_Services.MembershipService.Domain.Entities;
using GienPhim_Services.MembershipService.Domain.Services.Interfaces;

namespace GienPhim_Services.MembershipService.Application.Orchestrators
{
    public class PlanOptionOrchestrator : IPlanOptionOrchestrator
    {
        private readonly IPlanOptionFactory _planOptionFactory;
        private readonly IPlanOptionDomainService _planOptionDomainService;
        private readonly ILogger<PlanOptionOrchestrator> _logger;

        public PlanOptionOrchestrator(
            IPlanOptionFactory planOptionFactory,
            IPlanOptionDomainService planOptionDomainService,
            ILogger<PlanOptionOrchestrator> logger)
        {
            _planOptionFactory = planOptionFactory;
            _planOptionDomainService = planOptionDomainService;
            _logger = logger;
        }

        // Tạo lựa chọn gói thành viên mới
        public async Task<PlanOption> CreatePlanOptionAsync(PlanOptionCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreatePlanOptionAsync với PlanId={PlanId}", createDto.PlanId);

            var planOption = _planOptionFactory.CreatePlanOption(createDto);

            _planOptionDomainService.EnsureCanCreate(planOption);

            _logger.LogInformation("Orchestrator: PlanOption {OptionId} thuộc PlanId={PlanId} đã được validate và tạo entity thành công", planOption.OptionId, planOption.PlanId);

            return planOption;
        }

        // Cập nhật lựa chọn gói thành viên
        public async Task<PlanOption> UpdatePlanOptionAsync(PlanOptionUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdatePlanOptionAsync với OptionId={OptionId}", updateDto.OptionId);

            var planOption = _planOptionFactory.UpdatePlanOption(updateDto);

            _planOptionDomainService.EnsureCanUpdate(planOption);

            _logger.LogInformation("Orchestrator: PlanOption OptionId={OptionId} đã được cập nhật thành công", updateDto.OptionId);
            return planOption;
        }

        // Xoá lựa chọn gói thành viên
        public async Task DeletePlanOptionAsync(PlanOptionDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeletePlanOptionAsync với OptionId={OptionId}", deleteDto.OptionId);

            var planOption = _planOptionFactory.DeletePlanOption(deleteDto);

            _planOptionDomainService.EnsureCanDelete(planOption);

            _logger.LogInformation("Orchestrator: PlanOption OptionId={OptionId} đã được xoá thành công", deleteDto.OptionId);
        }
    }
}
