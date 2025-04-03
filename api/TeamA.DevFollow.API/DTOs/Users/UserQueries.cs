﻿using System.Linq.Expressions;
using TeamA.DevFollow.API.Entities;

namespace TeamA.DevFollow.API.DTOs.Users;

public static class UserQueries
{
    public static Expression<Func<User, UserDto>> ProjectToDto()
    {
        return u => new UserDto
        {
            Id = u.Id,
            Email = u.Email,
            Name = u.Name,
            CreatedAtUtc = u.CreatedAtUtc,
            UpdatedAtUtc = u.UpdatedAtUtc
        };
    }
}
