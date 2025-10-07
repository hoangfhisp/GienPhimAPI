using GienPhim_Services.MembershipService.Application.DTOs.Client.PlanOption;
using GienPhim_Services.MembershipService.Application.DTOs.Common;
using GienPhim_Services.MembershipService.Application.Exceptions.Failed;
using GienPhim_Services.MembershipService.Application.Exceptions.NotFounds;
using GienPhim_Services.MembershipService.Application.Interfaces.Mappers;
using GienPhim_Services.MembershipService.Application.Interfaces.Orchestrators;
using GienPhim_Services.MembershipService.Application.Interfaces.Repositories;
using GienPhim_Services.MembershipService.Application.Interfaces.Services;

namespace GienPhim_Services.MembershipService.Application.Services
{
    public class PlanOptionService : IPlanOptionService
    {
        private readonly IPlanOptionOrchestrator _planOptionOrchestrator;
        private readonly IPlanOptionResponseMapper _planOptionResponseMapper;
        private readonly IPaginationMapper _paginationMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PlanOptionService> _logger;

        public PlanOptionService(
            IUnitOfWork unitOfWork,
            IPlanOptionOrchestrator planOptionOrchestrator,
            IPlanOptionResponseMapper planOptionResponseMapper,
            IPaginationMapper paginationMapper,
            ILogger<PlanOptionService> logger)
        {
            _unitOfWork = unitOfWork;
            _planOptionOrchestrator = planOptionOrchestrator;
            _planOptionResponseMapper = planOptionResponseMapper;
            _paginationMapper = paginationMapper;
            _logger = logger;
        }

        // Lấy danh sách PlanOption có phân trang
        public async Task<ResultListDTO<List<PlanOptionResponseDTO>>> GetAllPlanOptionsAsync(int page, int limit, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetAllPlanOptionsAsync với page={Page}, limit={Limit}", page, limit);

            var (planOptions, total, last) = await _unitOfWork.PlanOptions.GetAllPlanOptionsAsync(page, limit, cancellationToken);

            if (total == 0)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy PlanOption nào");
                throw new PlanOptionNotFoundException();
            }

            var dtoList = _planOptionResponseMapper.ToDtoList(planOptions);
            var pagination = _paginationMapper.ToDto(page, limit, total, last);

            _logger.LogInformation("ApplicationService: Lấy danh sách PlanOption thành công, tổng={Total}", total);

            return ResultListDTO<List<PlanOptionResponseDTO>>.Success(
                dtoList,
                "Lấy danh sách lựa chọn gói thành viên thành công",
                pagination
            );
        }

        // Lấy chi tiết 1 PlanOption
        public async Task<ResultDTO<PlanOptionResponseDTO>> GetDetailAsync(string optionId, CancellationToken ct = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu GetDetailAsync cho OptionId={OptionId}", optionId);

            var planOption = await _unitOfWork.PlanOptions.GetByIdAsync(optionId, ct);
            if (planOption is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy PlanOption Id={OptionId}", optionId);
                throw new PlanOptionNotFoundException(optionId);
            }

            var dto = _planOptionResponseMapper.ToDto(planOption);

            _logger.LogInformation("ApplicationService: Lấy chi tiết PlanOption Id={OptionId} thành công", optionId);

            return ResultDTO<PlanOptionResponseDTO>.Success(dto, "Lấy chi tiết lựa chọn gói thành viên thành công");
        }

        // Tạo mới PlanOption
        public async Task<ResultDTO> CreatePlanOptionAsync(PlanOptionCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu CreatePlanOptionAsync cho PlanId={PlanId}", createDto.PlanId);

            // Kiểm tra MembershipPlan tồn tại
            var existingPlan = await _unitOfWork.MembershipPlans.GetByIdAsync(createDto.PlanId, cancellationToken);
            if (existingPlan is null)
            {
                _logger.LogWarning("ApplicationService: Không tìm thấy MembershipPlan Id={PlanId}", createDto.PlanId);
                throw new MembershipPlanNotFoundException(createDto.PlanId);
            }

            // Tạo entity thông qua orchestrator
            var planOption = await _planOptionOrchestrator.CreatePlanOptionAsync(createDto, cancellationToken);

            // Transaction và lưu DB
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.PlanOptions.CreateAsync(planOption, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Tạo PlanOption thất bại cho PlanId={PlanId}", createDto.PlanId);
                await transaction.RollbackAsync(cancellationToken);
                throw new CreatePlanOptionFailedException(createDto.PlanId);
            }

            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("ApplicationService: Tạo PlanOption thành công cho PlanId={PlanId}", createDto.PlanId);

            return ResultDTO.Success($"Tạo lựa chọn gói thành viên cho PlanId={createDto.PlanId} thành công");
        }

        // Cập nhật PlanOption
        public async Task<ResultDTO<UpdatePlanOptionResponseDTO>> UpdatePlanOptionAsync(PlanOptionUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu UpdatePlanOptionAsync với OptionId={OptionId}", updateDto.OptionId);

            // Nếu DTO có PlanId => kiểm tra tồn tại MembershipPlan
            if (!string.IsNullOrEmpty(updateDto.PlanId))
            {
                var existingPlan = await _unitOfWork.MembershipPlans.GetByIdAsync(updateDto.PlanId, cancellationToken);
                if (existingPlan is null)
                {
                    _logger.LogWarning("ApplicationService: Không tìm thấy MembershipPlan Id={PlanId}", updateDto.PlanId);
                    throw new MembershipPlanNotFoundException(updateDto.PlanId);
                }
            }

            var planOption = await _planOptionOrchestrator.UpdatePlanOptionAsync(updateDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await _unitOfWork.PlanOptions.UpdateAsync(planOption, cancellationToken);
            if (result is null)
            {
                _logger.LogError("ApplicationService: Cập nhật PlanOption thất bại OptionId={OptionId}", updateDto.OptionId);
                await transaction.RollbackAsync(cancellationToken);
                throw new UpdatePlanOptionFailedException(updateDto.OptionId);
            }

            await transaction.CommitAsync(cancellationToken);

            var response = _planOptionResponseMapper.ToUpdateDto(planOption);

            _logger.LogInformation("ApplicationService: Cập nhật PlanOption thành công OptionId={OptionId}", updateDto.OptionId);

            return ResultDTO<UpdatePlanOptionResponseDTO>.Success(response, $"Cập nhật lựa chọn gói thành viên thành công");
        }

        // Xóa PlanOption
        public async Task<ResultDTO> DeletePlanOptionAsync(PlanOptionDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("ApplicationService: Bắt đầu DeletePlanOptionAsync với OptionId={OptionId}", deleteDto.OptionId);

            await _planOptionOrchestrator.DeletePlanOptionAsync(deleteDto, cancellationToken);

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var planOption = await _unitOfWork.PlanOptions.GetByIdAsync(deleteDto.OptionId, cancellationToken);
            if (planOption is null)
            {
                _logger.LogWarning("ApplicationService: PlanOption không tồn tại khi xóa OptionId={OptionId}", deleteDto.OptionId);
                return ResultDTO.Fail($"Không tìm thấy lựa chọn gói thành viên với Id {deleteDto.OptionId}");
            }

            await _unitOfWork.PlanOptions.DeleteAsync(planOption, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("ApplicationService: Xóa PlanOption thành công OptionId={OptionId}", deleteDto.OptionId);
            return ResultDTO.Success($"Xóa lựa chọn gói thành viên {deleteDto.OptionId} thành công");
        }
    }
}
