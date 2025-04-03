using Microsoft.AspNetCore.Identity;

namespace TeamA.DevFollow.API.Entities;

public sealed class User
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
    public string IdentityId { get; set; }
}

public sealed class RefreshToken
{
    public Guid Id { get; set; }
    public required string UserId { get; set; }
    public required string Token { get; set; }
    public required DateTime ExpiresAtUtc { get; set; }
    public IdentityUser User { get; set; }
}
