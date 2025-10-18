using AbySalto.Mid.Application.DTOs.Auth;

namespace AbySalto.Mid.Application.Services.Interfaces;

/// <summary>
/// Service interface for authentication operations
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new user and returns authentication response with JWT token
    /// </summary>
    /// <param name="registerDto">Registration data</param>
    /// <returns>Authentication response with token</returns>
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);

    /// <summary>
    /// Authenticates a user and returns JWT token
    /// </summary>
    /// <param name="loginDto">Login credentials</param>
    /// <returns>Authentication response with token</returns>
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);

    /// <summary>
    /// Gets current user information by user ID
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>User information or null if not found</returns>
    Task<AuthResponseDto?> GetCurrentUserAsync(string userId);
}
