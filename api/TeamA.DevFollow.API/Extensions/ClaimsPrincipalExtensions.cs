using System.Security.Claims;

namespace TeamA.DevFollow.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetIdentityId(this ClaimsPrincipal principal)
    {
        string? identityId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        return identityId;
    }
}
