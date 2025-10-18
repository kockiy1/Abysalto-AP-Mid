using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Domain.Interfaces;

/// <summary>
/// Repository interface for Basket entity with basket-specific operations
/// </summary>
public interface IBasketRepository : IGenericRepository<Basket>
{
    /// <summary>
    /// Gets a user's basket with all items included
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>Basket or null if not found</returns>
    Task<Basket?> GetByUserIdAsync(string userId);

    /// <summary>
    /// Adds an item to the basket
    /// </summary>
    /// <param name="item">Basket item to add</param>
    Task AddItemAsync(BasketItem item);

    /// <summary>
    /// Updates the quantity of a basket item
    /// </summary>
    /// <param name="itemId">Basket item ID</param>
    /// <param name="quantity">New quantity</param>
    Task UpdateItemQuantityAsync(int itemId, int quantity);

    /// <summary>
    /// Removes an item from the basket
    /// </summary>
    /// <param name="itemId">Basket item ID to remove</param>
    Task RemoveItemAsync(int itemId);

    /// <summary>
    /// Clears all items from the basket
    /// </summary>
    /// <param name="basketId">Basket ID</param>
    Task ClearBasketAsync(int basketId);
}
