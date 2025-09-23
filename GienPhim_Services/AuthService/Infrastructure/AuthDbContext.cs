using GienPhim_Services.AuthService.Application.Interfaces.Repositories;
using GienPhim_Services.AuthService.Domain.Entities;
using GienPhim_Services.AuthService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GienPhim_Services.AuthService.Infrastructure
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        // Bắt đầu một giao dịch mới
        public async Task<IEfTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            IDbContextTransaction transaction = await Database.BeginTransactionAsync(cancellationToken);
            return new EfTransaction(transaction);
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }


    }
}
