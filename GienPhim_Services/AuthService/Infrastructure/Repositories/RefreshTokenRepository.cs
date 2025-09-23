using GienPhim_Services.AuthService.Application.Interfaces.Repositories;
using GienPhim_Services.AuthService.Domain.Entities;

namespace GienPhim_Services.AuthService.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AuthDbContext _context;
        public RefreshTokenRepository(AuthDbContext context)
        {
            _context = context;
        }

        // Thêm mới Refresh Token
        public async Task<RefreshToken> AddRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return refreshToken;
        }

    }
}
