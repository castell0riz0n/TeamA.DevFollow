using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using TeamA.DevFollow.API.Database.Contexts;
using TeamA.DevFollow.API.DTOs.Auth;
using TeamA.DevFollow.API.DTOs.Users;
using TeamA.DevFollow.API.Entities;
using TeamA.DevFollow.API.Options;
using TeamA.DevFollow.API.Services;

namespace TeamA.DevFollow.API.Controllers;

[ApiController]
[Route("auth")]
[AllowAnonymous]
public sealed class AuthController(
    UserManager<IdentityUser> userManager,
    ApplicationIdentityDbContext identityDbContext,
    ApplicationDbContext applicationDbContext,
    TokenProvider tokenProvider,
    IOptions<JwtAuthOptions> options
    ) : ControllerBase
{

    private readonly JwtAuthOptions _jwtOptions = options.Value;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
    {

        using IDbContextTransaction transaction = await identityDbContext.Database.BeginTransactionAsync();

        applicationDbContext.Database.SetDbConnection(identityDbContext.Database.GetDbConnection());

        await applicationDbContext.Database.UseTransactionAsync(transaction.GetDbTransaction());

        var identityUser = new IdentityUser
        {
            Email = request.Email,
            UserName = request.Email
        };

        IdentityResult createUserResult = await userManager.CreateAsync(identityUser, request.Password);

        if (!createUserResult.Succeeded)
        {
            var extensions = new Dictionary<string, object?>
            {
                { "errors", createUserResult.Errors.ToDictionary(e => e.Code, e => e.Description) }
            };
            return Problem(
                detail: "Unable to register user, please try again",
                statusCode: StatusCodes.Status400BadRequest,
                extensions: extensions
            );
        }

        IdentityResult addToRoleResult = await userManager.AddToRoleAsync(identityUser, Roles.Member);


        if (!addToRoleResult.Succeeded)
        {
            var extensions = new Dictionary<string, object?>
            {
                { "errors", addToRoleResult.Errors.ToDictionary(e => e.Code, e => e.Description) }
            };
            return Problem(
                detail: "Unable to register user, please try again",
                statusCode: StatusCodes.Status400BadRequest,
                extensions: extensions
            );
        }


        User user = request.ToEntity();
        user.IdentityId = identityUser.Id;

        applicationDbContext.Users.Add(user);
        await applicationDbContext.SaveChangesAsync();

        AccessTokenDto accessToken = tokenProvider.Create(new TokenRequest(identityUser.Id, user.Email, [Roles.Member]));

        var refreshToken = new RefreshToken
        {
            Id = Guid.CreateVersion7(),
            UserId = identityUser.Id,
            Token = accessToken.RefreshToken,
            ExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationDays)
        };

        identityDbContext.RefreshTokens.Add(refreshToken);
        await identityDbContext.SaveChangesAsync();

        await transaction.CommitAsync();

        return Ok(accessToken);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AccessTokenDto>> Login([FromBody] LoginUserDto request)
    {
        IdentityUser? user = await userManager.FindByEmailAsync(request.Email);
        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            return Unauthorized();
        }

        IList<string> roles = await userManager.GetRolesAsync(user);

        AccessTokenDto accessToken = tokenProvider.Create(new TokenRequest(user.Id, user.Email!, roles));

        var refreshToken = new RefreshToken
        {
            Id = Guid.CreateVersion7(),
            UserId = user.Id,
            Token = accessToken.RefreshToken,
            ExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationDays)
        };

        identityDbContext.RefreshTokens.Add(refreshToken);
        await identityDbContext.SaveChangesAsync();


        return Ok(accessToken);
    }
    [HttpPost("refresh")]
    public async Task<ActionResult<AccessTokenDto>> Refresh([FromBody] RefreshTokenDto request)
    {
        RefreshToken? refreshToken = await identityDbContext.RefreshTokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Token == request.RefreshToken);

        if (refreshToken is null || refreshToken.ExpiresAtUtc < DateTime.UtcNow)
        {
            return Unauthorized();
        }

        IList<string> roles = await userManager.GetRolesAsync(refreshToken.User);

        AccessTokenDto accessToken = tokenProvider.Create(new TokenRequest(refreshToken.User.Id, refreshToken.User.Email!, roles));
        refreshToken.Token = accessToken.RefreshToken;
        refreshToken.ExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationDays);
        await identityDbContext.SaveChangesAsync();
        return Ok(accessToken);
    }
}
