namespace AbySalto.Mid.Application.DTOs.Auth;

/// <summary>
/// DTO for authentication response (after successful login/register)
/// </summary>
public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
