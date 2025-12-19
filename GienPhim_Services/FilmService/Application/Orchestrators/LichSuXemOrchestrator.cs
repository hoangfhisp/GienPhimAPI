
using GienPhim_Services.FilmService.Application.DTOs.Client.LichSuXem;
using GienPhim_Services.FilmService.Application.DTOs.User;
using GienPhim_Services.FilmService.Application.Exceptions.Failed;
using GienPhim_Services.FilmService.Application.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Application.Interfaces.Factories;
using GienPhim_Services.FilmService.Application.Interfaces.Orchestrators;
using GienPhim_Services.FilmService.Application.Interfaces.Validators;
using GienPhim_Services.FilmService.Domain.Entities;
using GienPhim_Services.FilmService.Domain.Services.Interfaces;
using GienPhim_Services.FilmService.Infrastructure.Data.HttpClients.Interfaces;

namespace GienPhim_Services.FilmService.Application.Orchestrators
{
    public class LichSuXemOrchestrator : ILichSuXemOrchestrator
    {
        private readonly ILichSuXemFactory _lichSuXemFactory;
        private readonly ILichSuXemDomainService _lichSuXemDomainService;
        private readonly IValidationService _validationService;
        private readonly IUserServiceClient _userServiceClient;
        private readonly ILogger<LichSuXemOrchestrator> _logger;

        public LichSuXemOrchestrator(ILichSuXemFactory lichSuXemFactory, ILichSuXemDomainService lichSuXemDomainService
            ,IValidationService validationService, IUserServiceClient userServiceClient
            , ILogger<LichSuXemOrchestrator> logger)
        {
            _lichSuXemFactory = lichSuXemFactory;
            _lichSuXemDomainService = lichSuXemDomainService;
            _validationService = validationService;
            _userServiceClient = userServiceClient;
            _logger = logger;
        }

        // Tạo lịch sử xem mới
        public async Task<LichSuXem> CreateLichSuXem(LichSuXemCreateDTO createDTO, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Orchestrator: Bắt đầu CreateLichSuXem cho UserId={UserId}, PhimId={PhimId}, TapVuaXem={TapVuaXem}", createDTO.UserId, createDTO.PhimId, createDTO.TapVuaXem);

            var tapPhimId = await _validationService.ValidateAsync<LichSuXemCreateDTO,string>(createDTO,cancellationToken);

            var lichSuXem = _lichSuXemFactory.Create(createDTO, tapPhimId);

            _lichSuXemDomainService.EnsureCanCreate(lichSuXem);
            _logger.LogInformation("Orchestrator: Lịch sử xem cho UserId={UserId}, PhimId={PhimId}, TapPhimId={TapPhimId} đã validate OK", createDTO.UserId, createDTO.PhimId, lichSuXem.TapPhimId);
            return lichSuXem;
        }

        // Lấy thông tin user từ dịch vụ UserService theo userId
        public async Task<UserResponseDTO> FetchUserByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            var userResponse = await _userServiceClient.FindUserByUserIdAsync(userId, cancellationToken);

            if (userResponse == null)
            {
                _logger.LogError("ApplicationService: FetchUserByUserIdAsync không kết nối được UserService");
                throw new ConnectServiceFailedException("UserSerivce");
            }

            if (!userResponse.Data.Status)
            {
                _logger.LogError("ApplicationService: không tìm thấy user với userId {userId}", userId);
                throw new UserNotFoundException(userResponse.Data.Msg, userResponse.StatusCode);
            }

            return userResponse.Data.Data;
        }

    }
}
