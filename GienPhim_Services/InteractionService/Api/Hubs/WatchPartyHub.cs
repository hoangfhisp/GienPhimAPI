using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatGroup;
using Microsoft.AspNetCore.SignalR;

namespace GienPhim_Services.InteractionService.Api.Hubs
{
    public class WatchPartyHub : Hub
    {
        public async Task JoinRoom(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }

        public async Task ChangeEpisode(string groupId, EpisodeSyncDto episode)
        {
            await Clients.OthersInGroup(groupId)
                .SendAsync("ReceivePlayerEvent", "changeEpisode", episode);
        }

        public async Task SendPlayerEvent(string groupId, string eventType, double currentTime)
        {
            await Clients.OthersInGroup(groupId).SendAsync("ReceivePlayerEvent", eventType, currentTime);
        }

        public async Task SendMessage(string groupId, string userId, string message)
        {
            var msg = new { UserId = userId, Text = message, Timestamp = DateTime.UtcNow };
            await Clients.Group(groupId).SendAsync("ReceiveMessage", msg);
        }

    }
}

