using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Application.Services.Interfaces;

/// <summary>
/// Interface for generating authentication tokens
/// </summary>
public interface ITokenGenerator
{
    /// <summary>
    /// Generates a JWT token for a user
    /// </summary>
    /// <param name="user">Application user</param>
    /// <returns>JWT token string</returns>
    string GenerateToken(ApplicationUser user);
}
