using System.Security.Claims;
using AbySalto.Mid.Application.DTOs.Favorite;
using AbySalto.Mid.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.Controllers;

/// <summary>
/// Controller for favorite product operations (requires authentication)
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FavoriteController : ControllerBase
{
    private readonly IFavoriteProductService _favoriteProductService;

    public FavoriteController(IFavoriteProductService favoriteProductService)
    {
        _favoriteProductService = favoriteProductService;
    }

    /// <summary>
    /// Gets all favorite products for current user
    /// </summary>
    /// <returns>List of user's favorite products</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FavoriteProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<FavoriteProductDto>>> GetFavorites()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "User not authenticated." });
        }

        var favorites = await _favoriteProductService.GetUserFavoritesAsync(userId);
        return Ok(favorites);
    }

    /// <summary>
    /// Checks if a product is in user's favorites
    /// </summary>
    /// <param name="productId">Product ID to check</param>
    /// <returns>True if product is favorite, false otherwise</returns>
    [HttpGet("{productId}/check")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<bool>> IsFavorite(int productId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "User not authenticated." });
        }

        var isFavorite = await _favoriteProductService.IsFavoriteAsync(userId, productId);
        return Ok(isFavorite);
    }

    /// <summary>
    /// Adds a product to user's favorites
    /// </summary>
    /// <param name="productId">Product ID to add</param>
    /// <returns>No content</returns>
    [HttpPost("{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AddToFavorites(int productId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "User not authenticated." });
        }

        try
        {
            await _favoriteProductService.AddToFavoritesAsync(userId, productId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Removes a product from user's favorites
    /// </summary>
    /// <param name="productId">Product ID to remove</param>
    /// <returns>No content</returns>
    [HttpDelete("{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RemoveFromFavorites(int productId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "User not authenticated." });
        }

        try
        {
            await _favoriteProductService.RemoveFromFavoritesAsync(userId, productId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}