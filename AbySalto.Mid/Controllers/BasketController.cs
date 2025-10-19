using System.Security.Claims;
using AbySalto.Mid.Application.DTOs.Basket;
using AbySalto.Mid.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.Controllers;

/// <summary>
/// Controller for basket operations (requires authentication)
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    /// <summary>
    /// Gets current user's basket
    /// </summary>
    /// <returns>User's basket with all items</returns>
    [HttpGet]
    [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<BasketDto>> GetBasket()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "User not authenticated." });
        }

        var basket = await _basketService.GetUserBasketAsync(userId);

        if (basket == null)
        {
            return NotFound(new { message = "Basket not found." });
        }

        return Ok(basket);
    }

    /// <summary>
    /// Adds a product to user's basket
    /// </summary>
    /// <param name="addToBasketDto">Product and quantity to add</param>
    /// <returns>Updated basket</returns>
    [HttpPost("items")]
    [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<BasketDto>> AddToBasket([FromBody] AddToBasketDto addToBasketDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "User not authenticated." });
        }

        try
        {
            var basket = await _basketService.AddToBasketAsync(
                userId,
                addToBasketDto.ProductId,
                addToBasketDto.Quantity
            );

            return Ok(basket);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Updates quantity of a basket item
    /// </summary>
    /// <param name="itemId">Basket item ID</param>
    /// <param name="quantity">New quantity</param>
    /// <returns>Updated basket</returns>
    [HttpPut("items/{itemId}")]
    [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<BasketDto>> UpdateItemQuantity(int itemId, [FromBody] int quantity)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "User not authenticated." });
        }

        try
        {
            var basket = await _basketService.UpdateQuantityAsync(userId, itemId, quantity);
            return Ok(basket);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Removes an item from basket
    /// </summary>
    /// <param name="itemId">Basket item ID to remove</param>
    /// <returns>Updated basket</returns>
    [HttpDelete("items/{itemId}")]
    [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<BasketDto>> RemoveItem(int itemId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "User not authenticated." });
        }

        try
        {
            var basket = await _basketService.RemoveFromBasketAsync(userId, itemId);
            return Ok(basket);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Clears all items from basket
    /// </summary>
    /// <returns>No content</returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ClearBasket()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "User not authenticated." });
        }

        try
        {
            await _basketService.ClearBasketAsync(userId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}