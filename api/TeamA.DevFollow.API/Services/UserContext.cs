using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TeamA.DevFollow.API.Database.Contexts;
using TeamA.DevFollow.API.Extensions;

namespace TeamA.DevFollow.API.Services;

public sealed class UserContext(
     IHttpContextAccessor httpContextAccessor,
     ApplicationDbContext dbContext,
     IMemoryCache memoryCache
    )
{
    private const string CachKeyPrefix = "users:id:";
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(30);

    public async Task<string?> GetUserIdAsync(CancellationToken cancellationToken = default)
    {
        string? identityId = httpContextAccessor.HttpContext?.User.GetIdentityId();
        if (string.IsNullOrWhiteSpace(identityId))
        {
            return null;
        }

        string cacheKey = $"{CachKeyPrefix}{identityId}";
        string? userId = await memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SetSlidingExpiration(CacheExpiration);
            return await dbContext.Users
                .Where(a => a.IdentityId == identityId)
                .Select(a => a.Id)
                .FirstOrDefaultAsync(cancellationToken);
        });

        return userId;
    }
}
