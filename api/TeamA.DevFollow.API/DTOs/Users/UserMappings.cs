using TeamA.DevFollow.API.DTOs.Auth;
using TeamA.DevFollow.API.Entities;

namespace TeamA.DevFollow.API.DTOs.Users;

public static class UserMappings
{
    public static User ToEntity(this RegisterUserDto dto)
    {
        return new User
        {
            Id = "u_" + Guid.CreateVersion7(),
            Email = dto.Email,
            Name = dto.Name,
            CreatedAtUtc = DateTime.UtcNow
        };
    }
}
