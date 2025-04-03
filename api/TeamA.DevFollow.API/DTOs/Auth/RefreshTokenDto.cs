namespace TeamA.DevFollow.API.DTOs.Auth;

public sealed record RefreshTokenDto
{
    public required string RefreshToken { get; init; }
}
