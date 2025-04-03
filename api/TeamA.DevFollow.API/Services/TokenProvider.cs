﻿using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using TeamA.DevFollow.API.DTOs.Auth;
using TeamA.DevFollow.API.Options;

namespace TeamA.DevFollow.API.Services;

public sealed class TokenProvider(IOptions<JwtAuthOptions> options)
{
    private readonly JwtAuthOptions _jwtOptions = options.Value;

    public AccessTokenDto Create(TokenRequest tokenRequest)
    {
        return new AccessTokenDto(GenerateAccessToken(tokenRequest), GenerateRefreshToken());
    }

    private string GenerateAccessToken(TokenRequest tokenRequest)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = [
             new(JwtRegisteredClaimNames.Sub, tokenRequest.UserId),
             new(JwtRegisteredClaimNames.Email, tokenRequest.Email),
             ..tokenRequest.Roles.Select(role => new Claim(ClaimTypes.Role, role))
            ];

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationInMinutes),
            SigningCredentials = credentials,
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience
        };
        var handler = new JsonWebTokenHandler();

        string accessToken = handler.CreateToken(tokenDescriptor);

        return accessToken;
    }
    private string GenerateRefreshToken()
    {
        byte[] randomNumber = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(randomNumber);
    }
}
