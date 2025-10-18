using AbySalto.Mid.Domain.Interfaces;
using AbySalto.Mid.Infrastructure.Data;

namespace AbySalto.Mid.Infrastructure.Repositories;

/// <summary>
/// Unit of Work implementation - coordinates repositories and manages transactions
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    // Lazy initialization of repositories
    private IProductRepository? _products;
    private IBasketRepository? _baskets;
    private IFavoriteProductRepository? _favoriteProducts;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Product repository - lazy initialized
    /// </summary>
    public IProductRepository Products => _products ??= new ProductRepository(_context);

    /// <summary>
    /// Basket repository - lazy initialized
    /// </summary>
    public IBasketRepository Baskets => _baskets ??= new BasketRepository(_context);

    /// <summary>
    /// Favorite products repository - lazy initialized
    /// </summary>
    public IFavoriteProductRepository FavoriteProducts => _favoriteProducts ??= new FavoriteProductRepository(_context);

    /// <summary>
    /// Saves all changes to the database in a single transaction
    /// </summary>
    /// <returns>Number of affected records</returns>
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disposes the database context
    /// </summary>
    public void Dispose()
    {
        _context.Dispose();
    }
}
