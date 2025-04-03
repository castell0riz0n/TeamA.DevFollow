using TeamA.DevFollow.API.DTOs.Common;

namespace TeamA.DevFollow.API.DTOs.Tags;

public sealed record TagsCollectionDto : ICollectionResponse<TagDto>
{
    public List<TagDto> Items { get; init; }
    public List<LinkDto> Links { get; set; }
}
