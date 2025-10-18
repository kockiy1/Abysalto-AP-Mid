using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Interfaces;
using AbySalto.Mid.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Mid.Infrastructure.Repositories;

/// <summary>
/// Basket repository implementation with basket-specific operations
/// </summary>
public class BasketRepository : GenericRepository<Basket>, IBasketRepository
{
    public BasketRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Gets a user's basket with all items included (eager loading)
    /// </summary>
    public async Task<Basket?> GetByUserIdAsync(string userId)
    {
        return await _dbSet
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.UserId == userId);
    }

    /// <summary>
    /// Adds an item to the basket
    /// </summary>
    public async Task AddItemAsync(BasketItem item)
    {
        await _context.BasketItems.AddAsync(item);
    }

    /// <summary>
    /// Updates the quantity of a basket item
    /// </summary>
    public async Task UpdateItemQuantityAsync(int itemId, int quantity)
    {
        var item = await _context.BasketItems.FindAsync(itemId);
        if (item != null)
        {
            item.Quantity = quantity;
            _context.BasketItems.Update(item);
        }
    }

    /// <summary>
    /// Removes an item from the basket
    /// </summary>
    public async Task RemoveItemAsync(int itemId)
    {
        var item = await _context.BasketItems.FindAsync(itemId);
        if (item != null)
        {
            _context.BasketItems.Remove(item);
        }
    }

    /// <summary>
    /// Clears all items from the basket
    /// </summary>
    public async Task ClearBasketAsync(int basketId)
    {
        var items = await _context.BasketItems
            .Where(bi => bi.BasketId == basketId)
            .ToListAsync();

        _context.BasketItems.RemoveRange(items);
    }
}
