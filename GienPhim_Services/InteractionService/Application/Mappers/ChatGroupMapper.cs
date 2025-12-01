using GienPhim_Services.InteractionService.Application.DTOs.Client.ChatGroup;
using GienPhim_Services.InteractionService.Application.Interfaces.Mappers;
using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Mappers
{
    public class ChatGroupMapper : IChatGroupMapper
    {
        // Detail response
        public ChatGroupResponseDTO ToResponseDto(ChatGroup group)
        {
            return new ChatGroupResponseDTO
            {
                GroupId = group.GroupId,
                PhimId = group.PhimId,
                PhimName = group.PhimName,

                CreatedByUserId = group.CreatedByUserId,

                AllowChat = group.AllowChat,
                AllowInvite = group.AllowInvite,
                AllowJoinByLink = group.AllowJoinByLink,

                IsPlaying = group.IsPlaying,
                CurrentPlaybackPosition = group.CurrentPlaybackPosition,
                LastPlaybackUpdated = group.LastPlaybackUpdated,

                ShowStartTime = group.ShowStartTime,
                ShowEndTime = group.ShowEndTime,
                AutoDeleteAt = group.AutoDeleteAt,
                CreatedAt = group.CreatedAt,

                IsDeleted = group.IsDeleted,

                InviteCode = group.InviteCode,
                InviteCodeExpireAt = group.InviteCodeExpireAt,

                MemberCount = group.Members?.Count ?? 0
            };
        }

        public List<ChatGroupResponseDTO> ToResponseDtoList(List<ChatGroup> groups)
        {
            return groups.Select(ToResponseDto).ToList();
        }

        // List item response
        public ChatGroupListItemDTO ToListItemDto(ChatGroup group)
        {
            return new ChatGroupListItemDTO
            {
                GroupId = group.GroupId,

                PhimId = group.PhimId,
                PhimName = group.PhimName,

                CreatedByUserId = group.CreatedByUserId,

                IsDeleted = group.IsDeleted,

                ShowStartTime = group.ShowStartTime,
                ShowEndTime = group.ShowEndTime,

                AllowChat = group.AllowChat,
                AllowInvite = group.AllowInvite,
                AllowJoinByLink = group.AllowJoinByLink,

                MemberCount = group.Members?.Count ?? 0
            };
        }

        public List<ChatGroupListItemDTO> ToListItemDtoList(List<ChatGroup> groups)
        {
            return groups.Select(ToListItemDto).ToList();
        }
    }
}
