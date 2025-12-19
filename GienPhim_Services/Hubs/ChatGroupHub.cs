using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatGroup;
using GienPhim_Services.InteractionService.Application.Interfaces.Mappers;
using GienPhim_Services.InteractionService.Application.Interfaces.Orchestrators;
using GienPhim_Services.InteractionService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Application.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;

namespace GienPhim_Services.Hubs
{
    public class ChatGroupHub : Hub
    {
        private readonly IChatGroupRepository _repo;
        private readonly IChatGroupOrchestrator _orchestrator;
        private readonly IChatGroupResponseMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTimeProvider _clock;

        public async Task UpdateSettings(Guid groupId, ChatGroupUpdateSettingsDTO dto)
        {
            var userId = _currentUser.UserId;
            var now = _clock.UtcNow;

            var group = await _repo.GetByIdAsync(groupId);
            await _orchestrator.UpdateSettingsAsync(group!, dto, userId, now);

            await _repo.UpdateAsync(group!);

            var response = _mapper.ToResponseDto(group!);
            await Clients.Group(groupId.ToString()).SendAsync("SettingsUpdated", response);
        }
    }

}
