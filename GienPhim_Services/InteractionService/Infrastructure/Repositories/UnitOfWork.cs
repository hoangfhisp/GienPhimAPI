using GienPhim_Services.InteractionService.Application.Interfaces.Repositories;
using GienPhim_Services.InteractionService.Domain.Entities;
using GienPhim_Services.InteractionService.Infrastructure.Data;

namespace GienPhim_Services.InteractionService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InteractionDbContext _context;

        public UnitOfWork
        (
            InteractionDbContext context,
            IChatGroupRepository chatGroups,
            IGroupMemberRepository groupMembers,
            IJoinRequestRepository joinRequests,
            IChatMessageRepository chatMessages)
        {
            _context = context;
            ChatGroups = chatGroups;
            GroupMembers = groupMembers;
            JoinRequests = joinRequests;
            ChatMessages = chatMessages;
        }

        public IChatGroupRepository ChatGroups { get; }
        public IGroupMemberRepository GroupMembers { get; }
        public IJoinRequestRepository JoinRequests { get; }

        public IChatMessageRepository ChatMessages { get; }

        public async Task<IEfTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            return new EfTransaction(transaction);
        }
    }

}
