namespace AbySalto.Mid.Domain.Interfaces;

/// <summary>
/// Unit of Work pattern - coordinates repositories and manages transactions
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Product repository
    /// </summary>
    IProductRepository Products { get; }

    /// <summary>
    /// Basket repository
    /// </summary>
    IBasketRepository Baskets { get; }

    /// <summary>
    /// Favorite products repository
    /// </summary>
    IFavoriteProductRepository FavoriteProducts { get; }

    /// <summary>
    /// Saves all changes to the database in a single transaction
    /// </summary>
    /// <returns>Number of affected records</returns>
    Task<int> SaveChangesAsync();
}
