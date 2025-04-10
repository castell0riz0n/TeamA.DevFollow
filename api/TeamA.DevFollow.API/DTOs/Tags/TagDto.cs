using TeamA.DevFollow.API.DTOs.Common;

namespace TeamA.DevFollow.API.DTOs.Tags;

public sealed record TagDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required DateTime CreatedAtUtc { get; init; }
    public DateTime? UpdatedAtUtc { get; init; }
    public List<LinkDto> Links { get; set; }
}
