﻿using Microsoft.AspNetCore.Mvc;
using TeamA.DevFollow.API.DTOs.Common;
using TeamA.DevFollow.API.Entities;

namespace TeamA.DevFollow.API.DTOs.Habits;

public sealed record HabitsQueryParameters : AcceptHeaderDto
{
    [FromQuery(Name = "q")]
    public string? Search { get; set; }
    public HabitType? Type { get; init; }
    public HabitStatus? Status { get; init; }
    public string? Sort { get; init; }
    public string? Fields { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
