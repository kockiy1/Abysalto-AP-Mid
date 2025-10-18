using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Interfaces;
using AbySalto.Mid.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Mid.Infrastructure.Repositories;

/// <summary>
/// Favorite product repository implementation with favorite-specific operations
/// </summary>
public class FavoriteProductRepository : GenericRepository<FavoriteProduct>, IFavoriteProductRepository
{
    public FavoriteProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Gets all favorite products for a user with product details
    /// </summary>
    public async Task<IEnumerable<FavoriteProduct>> GetUserFavoritesAsync(string userId)
    {
        return await _dbSet
            .Include(fp => fp.Product)
            .Where(fp => fp.UserId == userId)
            .ToListAsync();
    }

    /// <summary>
    /// Checks if a product is marked as favorite by a user
    /// </summary>
    public async Task<bool> IsFavoriteAsync(string userId, int productId)
    {
        return await _dbSet
            .AnyAsync(fp => fp.UserId == userId && fp.ProductId == productId);
    }

    /// <summary>
    /// Adds a product to user's favorites
    /// </summary>
    public async Task AddFavoriteAsync(string userId, int productId)
    {
        var favorite = new FavoriteProduct
        {
            UserId = userId,
            ProductId = productId,
            AddedAt = DateTime.UtcNow
        };

        await _dbSet.AddAsync(favorite);
    }

    /// <summary>
    /// Removes a product from user's favorites
    /// </summary>
    public async Task RemoveFavoriteAsync(string userId, int productId)
    {
        var favorite = await _dbSet
            .FirstOrDefaultAsync(fp => fp.UserId == userId && fp.ProductId == productId);

        if (favorite != null)
        {
            _dbSet.Remove(favorite);
        }
    }
}
