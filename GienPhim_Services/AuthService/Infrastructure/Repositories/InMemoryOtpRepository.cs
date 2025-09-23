using GienPhim_Services.AuthService.Application.Interfaces.Repositories;
using GienPhim_Services.AuthService.Domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace GienPhim_Services.AuthService.Infrastructure.Repositories
{
    public class InMemoryOtpRepository : IOtpRepository
    {
        private readonly IMemoryCache _cache;

        public InMemoryOtpRepository(IMemoryCache cache)
        {
            _cache = cache;
        }
        private string BuildKey(string userId, string type) => $"otp:{type}:{userId}";

        public Task SaveAsync(Otp otp, CancellationToken cancellationToken = default)
        {
            var key = BuildKey(otp.UserId, otp.Type);
            _cache.Set(key, otp, otp.ExpiryTime); // set TTL = thời gian hết hạn của OTP
            return Task.CompletedTask;
        }

        public Task<Otp?> GetAsync(string userId, string type, CancellationToken cancellationToken = default)
        {
            var key = BuildKey(userId, type);
            _cache.TryGetValue<Otp>(key, out var otp);
            return Task.FromResult(otp);
        }

        public Task RemoveAsync(string userId, string type, CancellationToken cancellationToken = default)
        {
            var key = BuildKey(userId, type);
            _cache.Remove(key);
            return Task.CompletedTask;
        }
    }
}
