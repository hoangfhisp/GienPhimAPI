using GienPhim_Services.MembershipService.Application.DTOs.Client.MembershipPlan;
using GienPhim_Services.MembershipService.Application.DTOs.Common;
using GienPhim_Services.MembershipService.Application.Exceptions.Failed;
using GienPhim_Services.MembershipService.Application.Exceptions.NotFounds;
using GienPhim_Services.MembershipService.Application.Interfaces.Mappers;
using GienPhim_Services.MembershipService.Application.Interfaces.Repositories;
using GienPhim_Services.MembershipService.Application.Interfaces.Services;
using GienPhim_Services.MembershipService.Application.Interfaces.Orchestrators;
using GienPhim_Services.MembershipService.Application.Exceptions.Duplicates;

namespace GienPhim_Services.MembershipService.Application.Services
{
    public class MembershipPlanService : IMembershipPlanService
    {
        private readonly IMembershipPlanOrchestrator _orchestrator;
        private readonly IMembershipPlanResponseMapper _mapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MembershipPlanService> _logger;

        public MembershipPlanService(
            IUnitOfWork unitOfWork,
            IMembershipPlanOrchestrator orchestrator,
            IMembershipPlanResponseMapper mapper,
            IPaginationMapper paginationMapper,
            ILogger<MembershipPlanService> logger)
        {
            _unitOfWork = unitOfWork;
            _orchestrator = orchestrator;
            _mapper = mapper;
            _paginationMapper = paginationMapper;
            _logger = logger;
        }

        // Lấy danh sách MembershipPlans
        public async Task<ResultListDTO<List<MembershipPlanResponseDTO>>> GetAllMembershipPlansAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetAllMembershipPlansAsync với trang {Page}, giới hạn {Limit}", page, limit);

            var (plans, total, last) = await _unitOfWork.MembershipPlans.GetAllMembershipPlansAsync(page, limit, cancellationToken);

            if (total == 0)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy gói thành viên nào");
                throw new MembershipPlanNotFoundException();
            }

            var resultPlans = _mapper.ToDtoList(plans);
            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            return ResultListDTO<List<MembershipPlanResponseDTO>>.Success(resultPlans, "Lấy danh sách gói thành viên thành công", pagination);
        }

        // Lấy chi tiết theo Id
        public async Task<ResultDTO<MembershipPlanResponseDTO>> GetDetailAsync(string planId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetDetailAsync cho PlanId={PlanId}", planId);

            var plan = await _unitOfWork.MembershipPlans.GetByIdAsync(planId, cancellationToken);
            if (plan is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy gói thành viên Id={PlanId}", planId);
                throw new MembershipPlanNotFoundException(planId);
            }

            var dto = _mapper.ToDto(plan);
            return ResultDTO<MembershipPlanResponseDTO>.Success(dto, "Lấy chi tiết gói thành viên thành công");
        }

        // Tạo mới
        public async Task<ResultDTO> CreateMembershipPlanAsync(MembershipPlanCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu CreateMembershipPlanAsync với tên {TenPlan}", createDto.TenPlan);

            // Kiểm tra trùng tên trước khi tạo
            var existing = await _unitOfWork.MembershipPlans.FindByNameAsync(createDto.TenPlan, cancellationToken);
            if (existing is not null)
            {
                _logger.LogWarning("ApplicationService: Gói thành viên {TenPlan} đã tồn tại", createDto.TenPlan);
                throw new DuplicateMembershipPlanException(createDto.TenPlan);
            }

            // Orchestrator: tạo entity và validate
            var plan = await _orchestrator.CreateMembershipPlanAsync(createDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.MembershipPlans.CreateAsync(plan, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Tạo gói thành viên {TenPlan} thất bại!", plan.TenPlan);
                await transaction.RollbackAsync(cancellationToken);
                throw new CreateMembershipPlanFailedException(plan.TenPlan);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Tạo gói thành viên {TenPlan} thành công!", plan.TenPlan);

            return ResultDTO.Success($"Tạo gói thành viên {plan.TenPlan} thành công");
        }

        // Cập nhật
        public async Task<ResultDTO<UpdateMembershipPlanResponseDTO>> UpdateMembershipPlanAsync(MembershipPlanUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu UpdateMembershipPlanAsync với Id={PlanId}", updateDto.PlanId);

            var plan = await _orchestrator.UpdateMembershipPlanAsync(updateDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.MembershipPlans.UpdateAsync(plan, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Cập nhật gói thành viên {PlanId} thất bại!", updateDto.PlanId);
                await transaction.RollbackAsync(cancellationToken);
                throw new UpdateMembershipPlanFailedException(updateDto.PlanId);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Cập nhật gói thành viên {PlanId} thành công!", updateDto.PlanId);

            var response = _mapper.ToUpdateDto(plan);
            return ResultDTO<UpdateMembershipPlanResponseDTO>.Success(response, $"Cập nhật gói thành viên {plan.TenPlan} thành công");
        }

        // Xóa
        public async Task<ResultDTO> DeleteMembershipPlanAsync(MembershipPlanDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu DeleteMembershipPlanAsync với Id={PlanId}", deleteDto.PlanId);

            await _orchestrator.DeleteMembershipPlanAsync(deleteDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var plan = await _unitOfWork.MembershipPlans.GetByIdAsync(deleteDto.PlanId, cancellationToken);
            if (plan is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy gói thành viên {PlanId} để xoá", deleteDto.PlanId);
                return ResultDTO.Fail($"Không tìm thấy gói thành viên với Id {deleteDto.PlanId}");
            }

            await _unitOfWork.MembershipPlans.DeleteAsync(plan, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Xóa gói thành viên {PlanId} thành công!", deleteDto.PlanId);

            return ResultDTO.Success($"Xóa gói thành viên {plan.TenPlan} thành công");
        }
    }
}
