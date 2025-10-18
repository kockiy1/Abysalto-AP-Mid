using AbySalto.Mid.Application.DTOs.Basket;

namespace AbySalto.Mid.Application.Services.Interfaces;

/// <summary>
/// Service interface for basket operations
/// </summary>
public interface IBasketService
{
    /// <summary>
    /// Gets user's basket with all items
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>User's basket or null if not found</returns>
    Task<BasketDto?> GetUserBasketAsync(string userId);

    /// <summary>
    /// Adds a product to user's basket
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="productId">Product ID to add</param>
    /// <param name="quantity">Quantity to add</param>
    /// <returns>Updated basket</returns>
    Task<BasketDto> AddToBasketAsync(string userId, int productId, int quantity);

    /// <summary>
    /// Removes an item from user's basket
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="itemId">Basket item ID to remove</param>
    /// <returns>Updated basket</returns>
    Task<BasketDto> RemoveFromBasketAsync(string userId, int itemId);

    /// <summary>
    /// Updates the quantity of a basket item
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="itemId">Basket item ID</param>
    /// <param name="quantity">New quantity</param>
    /// <returns>Updated basket</returns>
    Task<BasketDto> UpdateQuantityAsync(string userId, int itemId, int quantity);

    /// <summary>
    /// Clears all items from user's basket
    /// </summary>
    /// <param name="userId">User ID</param>
    Task ClearBasketAsync(string userId);
}
