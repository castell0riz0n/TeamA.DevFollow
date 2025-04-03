using TeamA.DevFollow.API.DTOs.Common;

namespace TeamA.DevFollow.API.DTOs.Habits;

public sealed record HabitQueryParameters : AcceptHeaderDto
{
    public string? Fields { get; init; }
}
