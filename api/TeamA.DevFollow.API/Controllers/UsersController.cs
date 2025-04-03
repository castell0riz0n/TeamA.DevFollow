using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamA.DevFollow.API.Database;
using TeamA.DevFollow.API.DTOs.Users;

namespace TeamA.DevFollow.API.Controllers;

[ApiController]
[Route("users")]
public class UsersController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(string id)
    {
        UserDto? user = await dbContext.Users
            .Where(a => a.Id == id)
            .Select(UserQueries.ProjectToDto())
            .FirstOrDefaultAsync();

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}
