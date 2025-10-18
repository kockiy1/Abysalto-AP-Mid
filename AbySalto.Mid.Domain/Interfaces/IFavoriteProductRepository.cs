using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Domain.Interfaces;

/// <summary>
/// Repository interface for FavoriteProduct entity with favorite-specific operations
/// </summary>
public interface IFavoriteProductRepository : IGenericRepository<FavoriteProduct>
{
    /// <summary>
    /// Gets all favorite products for a user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>List of user's favorite products</returns>
    Task<IEnumerable<FavoriteProduct>> GetUserFavoritesAsync(string userId);

    /// <summary>
    /// Checks if a product is marked as favorite by a user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="productId">Product ID</param>
    /// <returns>True if product is favorite, false otherwise</returns>
    Task<bool> IsFavoriteAsync(string userId, int productId);

    /// <summary>
    /// Adds a product to user's favorites
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="productId">Product ID</param>
    Task AddFavoriteAsync(string userId, int productId);

    /// <summary>
    /// Removes a product from user's favorites
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="productId">Product ID</param>
    Task RemoveFavoriteAsync(string userId, int productId);
}
