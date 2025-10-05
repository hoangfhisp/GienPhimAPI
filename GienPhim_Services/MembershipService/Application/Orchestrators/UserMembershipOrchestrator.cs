using GienPhim_Services.MembershipService.Application.DTOs.Client.UserMembership;
using GienPhim_Services.MembershipService.Application.Interfaces.Factories;
using GienPhim_Services.MembershipService.Application.Interfaces.Orchestrators;
using GienPhim_Services.MembershipService.Domain.Entities;
using GienPhim_Services.MembershipService.Domain.Services.Interfaces;

namespace GienPhim_Services.MembershipService.Application.Orchestrators
{
    public class UserMembershipOrchestrator : IUserMembershipOrchestrator
    {
        private readonly IUserMembershipFactory _userMembershipFactory;
        private readonly IUserMembershipDomainService _userMembershipDomainService;
        private readonly ILogger<UserMembershipOrchestrator> _logger;

        public UserMembershipOrchestrator(
            IUserMembershipFactory userMembershipFactory,
            IUserMembershipDomainService userMembershipDomainService,
            ILogger<UserMembershipOrchestrator> logger)
        {
            _userMembershipFactory = userMembershipFactory;
            _userMembershipDomainService = userMembershipDomainService;
            _logger = logger;
        }

        // Tạo mới UserMembership
        public async Task<UserMembership> CreateUserMembershipAsync(UserMembershipCreateDTO createDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateUserMembershipAsync cho UserId={UserId}, OptionId={OptionId}", createDto.UserId, createDto.OptionId);

            // Factory khởi tạo entity từ DTO
            var userMembership = _userMembershipFactory.CreateUserMembership(createDto);

            // Domain kiểm tra nghiệp vụ
            _userMembershipDomainService.EnsureCanCreate(userMembership);

            _logger.LogInformation("Orchestrator: UserMembership cho UserId={UserId} đã được validate và tạo entity thành công", createDto.UserId);
            return userMembership;
        }

        // Cập nhật UserMembership
        public async Task<UserMembership> UpdateUserMembershipAsync(UserMembershipUpdateDTO updateDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu UpdateUserMembershipAsync với UserMembershipId={UserMembershipId}", updateDto.UserMembershipId);

            var userMembership = _userMembershipFactory.UpdateUserMembership(updateDto);

            _userMembershipDomainService.EnsureCanUpdate(userMembership);

            _logger.LogInformation("Orchestrator: UserMembership {UserMembershipId} đã được cập nhật thành công", updateDto.UserMembershipId);
            return userMembership;
        }

        // Xoá UserMembership
        public async Task DeleteUserMembershipAsync(UserMembershipDeleteDTO deleteDto, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu DeleteUserMembershipAsync với UserMembershipId={UserMembershipId}", deleteDto.UserMembershipId);

            var userMembership = _userMembershipFactory.DeleteUserMembership(deleteDto);

            _userMembershipDomainService.EnsureCanDelete(userMembership);

            _logger.LogInformation("Orchestrator: UserMembership {UserMembershipId} đã được xoá thành công", deleteDto.UserMembershipId);
        }
    }
}
