using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AbySalto.Mid.Application.Services.Interfaces;
using AbySalto.Mid.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AbySalto.Mid.Infrastructure.Authentication;

/// <summary>
/// Generates JWT tokens for authenticated users
/// </summary>
public class JwtTokenGenerator : ITokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    /// <summary>
    /// Generates a JWT token for a user
    /// </summary>
    /// <param name="user">Application user</param>
    /// <returns>JWT token string</returns>
    public string GenerateToken(ApplicationUser user)
    {
        // Create claims (user information stored in token)
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? string.Empty),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Create signing credentials
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create token
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            signingCredentials: credentials
        );

        // Return token string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
