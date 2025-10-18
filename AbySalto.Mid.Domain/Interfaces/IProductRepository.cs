using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Domain.Interfaces;

/// <summary>
/// Repository interface for Product entity with specific operations
/// </summary>
public interface IProductRepository : IGenericRepository<Product>
{
    /// <summary>
    /// Gets all products in a specific category
    /// </summary>
    /// <param name="category">Category name</param>
    /// <returns>List of products in the category</returns>
    Task<IEnumerable<Product>> GetByCategoryAsync(string category);

    /// <summary>
    /// Searches products by title or description
    /// </summary>
    /// <param name="searchTerm">Search term</param>
    /// <returns>List of matching products</returns>
    Task<IEnumerable<Product>> SearchAsync(string searchTerm);

    /// <summary>
    /// Gets multiple products by their IDs
    /// </summary>
    /// <param name="ids">Collection of product IDs</param>
    /// <returns>List of products</returns>
    Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> ids);
}
