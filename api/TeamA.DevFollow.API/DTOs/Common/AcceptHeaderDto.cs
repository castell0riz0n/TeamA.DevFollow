﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using TeamA.DevFollow.API.Services;

namespace TeamA.DevFollow.API.DTOs.Common;

public record AcceptHeaderDto
{
    [FromHeader(Name = "Accept")]
    public string? Accept { get; init; }

    public bool IncludeLinks => 
        MediaTypeHeaderValue.TryParse(Accept, out MediaTypeHeaderValue? mediaType) &&
        mediaType.SubTypeWithoutSuffix.HasValue &&
        mediaType.SubTypeWithoutSuffix.Value.Contains(CustomMediaTypeNames.Application.HateoasSubType)
    ;
}
