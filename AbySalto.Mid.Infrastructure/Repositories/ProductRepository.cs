using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Interfaces;
using AbySalto.Mid.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Mid.Infrastructure.Repositories;

/// <summary>
/// Product repository implementation with product-specific operations
/// </summary>
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Gets all products in a specific category
    /// </summary>
    public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
    {
        return await _dbSet
            .Where(p => p.Category == category)
            .ToListAsync();
    }

    /// <summary>
    /// Searches products by title or description
    /// </summary>
    public async Task<IEnumerable<Product>> SearchAsync(string searchTerm)
    {
        return await _dbSet
            .Where(p => p.Title.Contains(searchTerm) ||
                       p.Description.Contains(searchTerm))
            .ToListAsync();
    }

    /// <summary>
    /// Gets multiple products by their IDs
    /// </summary>
    public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> ids)
    {
        return await _dbSet
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();
    }
}
