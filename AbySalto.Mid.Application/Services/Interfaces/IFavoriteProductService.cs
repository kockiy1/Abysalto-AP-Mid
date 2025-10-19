using AbySalto.Mid.Application.DTOs.Favorite;

namespace AbySalto.Mid.Application.Services.Interfaces;

/// <summary>
/// Service interface for favorite product operations
/// </summary>
public interface IFavoriteProductService
{
    /// <summary>
    /// Gets all favorite products for a user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>List of user's favorite products</returns>
    Task<IEnumerable<FavoriteProductDto>> GetUserFavoritesAsync(string userId);

    /// <summary>
    /// Checks if a product is in user's favorites
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="productId">Product ID</param>
    /// <returns>True if product is favorite, false otherwise</returns>
    Task<bool> IsFavoriteAsync(string userId, int productId);

    /// <summary>
    /// Adds a product to user's favorites
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="productId">Product ID to add</param>
    /// <returns>The added favorite product details</returns>
    Task<FavoriteProductDto> AddToFavoritesAsync(string userId, int productId);

    /// <summary>
    /// Removes a product from user's favorites
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="productId">Product ID to remove</param>
    Task RemoveFromFavoritesAsync(string userId, int productId);
}
