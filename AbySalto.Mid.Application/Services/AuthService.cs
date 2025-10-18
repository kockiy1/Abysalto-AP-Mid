using AbySalto.Mid.Application.DTOs.Auth;
using AbySalto.Mid.Application.Services.Interfaces;
using AbySalto.Mid.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AbySalto.Mid.Application.Services;

/// <summary>
/// Service for authentication operations with business logic
/// </summary>
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IMapper _mapper;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        ITokenGenerator tokenGenerator,
        IMapper mapper)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
        _mapper = mapper;
    }

    /// <summary>
    /// Registers a new user and returns JWT token
    /// </summary>
    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        // Check if user already exists
        var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("User with this email already exists.");
        }

        // Create new user
        var user = new ApplicationUser
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            CreatedAt = DateTime.UtcNow
        };

        // Create user with password
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"User registration failed: {errors}");
        }

        // Generate JWT token
        var token = _tokenGenerator.GenerateToken(user);

        // Map to response DTO
        var response = _mapper.Map<AuthResponseDto>(user);
        response.Token = token;

        return response;
    }

    /// <summary>
    /// Authenticates user and returns JWT token
    /// </summary>
    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        // Find user by email
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            throw new InvalidOperationException("Invalid email or password.");
        }

        // Check password
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isPasswordValid)
        {
            throw new InvalidOperationException("Invalid email or password.");
        }

        // Generate JWT token
        var token = _tokenGenerator.GenerateToken(user);

        // Map to response DTO
        var response = _mapper.Map<AuthResponseDto>(user);
        response.Token = token;

        return response;
    }

    /// <summary>
    /// Gets current user information
    /// </summary>
    public async Task<AuthResponseDto?> GetCurrentUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return null;
        }

        // Generate new token for current user
        var token = _tokenGenerator.GenerateToken(user);

        // Map to response DTO
        var response = _mapper.Map<AuthResponseDto>(user);
        response.Token = token;

        return response;
    }
}
