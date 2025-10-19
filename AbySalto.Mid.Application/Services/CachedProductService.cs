using AbySalto.Mid.Application.DTOs.Product;
using AbySalto.Mid.Application.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace AbySalto.Mid.Application.Services;

/// <summary>
/// Decorator for ProductService that adds caching functionality using in-memory cache
/// </summary>
public class CachedProductService : IProductService
{
    private readonly IProductService _productService;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

    // Cache key constants
    private const string AllProductsCacheKey = "products_all";
    private const string ProductByIdCacheKeyPrefix = "product_";
    private const string ProductsByCategoryCacheKeyPrefix = "products_category_";
    private const string ProductsSearchCacheKeyPrefix = "products_search_";

    public CachedProductService(IProductService productService, IMemoryCache cache)
    {
        _productService = productService;
        _cache = cache;
    }

    /// <summary>
    /// Gets all products with caching
    /// </summary>
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        return await _cache.GetOrCreateAsync(AllProductsCacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
            return await _productService.GetAllProductsAsync();
        }) ?? Enumerable.Empty<ProductDto>();
    }

    /// <summary>
    /// Gets product by ID with caching
    /// </summary>
    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var cacheKey = $"{ProductByIdCacheKeyPrefix}{id}";

        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
            return await _productService.GetProductByIdAsync(id);
        });
    }

    /// <summary>
    /// Searches products by term with caching
    /// </summary>
    public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
    {
        var cacheKey = $"{ProductsSearchCacheKeyPrefix}{searchTerm.ToLowerInvariant()}";

        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
            return await _productService.SearchProductsAsync(searchTerm);
        }) ?? Enumerable.Empty<ProductDto>();
    }

    /// <summary>
    /// Gets products by category with caching
    /// </summary>
    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(string category)
    {
        var cacheKey = $"{ProductsByCategoryCacheKeyPrefix}{category.ToLowerInvariant()}";

        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
            return await _productService.GetProductsByCategoryAsync(category);
        }) ?? Enumerable.Empty<ProductDto>();
    }

    /// <summary>
    /// Clears all product-related cache entries
    /// </summary>
    public void ClearCache()
    {
        _cache.Remove(AllProductsCacheKey);
        // Note: Individual product, category, and search caches will expire naturally
        // For more aggressive invalidation, we could track all cache keys
    }
}