using AbySalto.Mid.Application.DTOs.Product;

namespace AbySalto.Mid.Application.Services.Interfaces;

/// <summary>
/// Service interface for product operations
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Gets all products
    /// </summary>
    /// <returns>List of all products</returns>
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();

    /// <summary>
    /// Gets a product by ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Product or null if not found</returns>
    Task<ProductDto?> GetProductByIdAsync(int id);

    /// <summary>
    /// Searches products by title or description
    /// </summary>
    /// <param name="searchTerm">Search term</param>
    /// <returns>List of matching products</returns>
    Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm);

    /// <summary>
    /// Gets products by category
    /// </summary>
    /// <param name="category">Category name</param>
    /// <returns>List of products in the category</returns>
    Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(string category);
}
