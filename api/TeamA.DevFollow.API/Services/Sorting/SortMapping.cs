﻿namespace TeamA.DevFollow.API.Services.Sorting;

public sealed record SortMapping(string SortField, string PropertyName, bool Reverse = false);
