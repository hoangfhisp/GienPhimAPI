using GienPhim_Services.InteractionService.Domain.Entities;

namespace GienPhim_Services.InteractionService.Application.Interfaces.Repositories
{
    public interface IChatGroupRepository
    {
        Task<ChatGroup?> GetByIdAsync(Guid groupId, CancellationToken cancellationToken = default);

        Task<(List<ChatGroup> groups, int total, int last)> GetByFilmAsync(int phimId,string? status,int page,int pageSize,CancellationToken cancellationToken = default);

        Task<(List<ChatGroup> groups, int total, int last)> GetMineAsync(string userId,string? role,string? status,int page,int pageSize,CancellationToken cancellationToken = default);

        Task<ChatGroup> CreateAsync(ChatGroup group, CancellationToken cancellationToken = default);

        Task<ChatGroup> UpdateAsync(ChatGroup group, CancellationToken cancellationToken = default);
    }
}
