using System.Text.Json;
using AbySalto.Mid.Application.DTOs;
using AbySalto.Mid.Application.Services.Interfaces;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace AbySalto.Mid.Application.Services;

/// <summary>
/// Service for integrating with DummyJSON API to fetch and sync products
/// </summary>
public class DummyJsonService : IDummyJsonService
{
    private readonly HttpClient _httpClient;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;
    private const string BaseUrl = "https://dummyjson.com";
    private const string AllProductsCacheKey = "products_all";

    public DummyJsonService(HttpClient httpClient, IUnitOfWork unitOfWork, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUrl);
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    /// <summary>
    /// Fetches products from DummyJSON API and syncs them to the database
    /// </summary>
    public async Task<int> SyncProductsAsync(int limit = 100)
    {
        try
        {
            // Fetch products from DummyJSON API
            var response = await _httpClient.GetAsync($"/products?limit={limit}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var productsResponse = JsonSerializer.Deserialize<DummyJsonProductsResponseDto>(content);

            if (productsResponse == null || !productsResponse.Products.Any())
            {
                return 0;
            }

            // Get existing product IDs to avoid duplicates
            var existingProducts = await _unitOfWork.Products.GetAllAsync();
            var existingIds = existingProducts.Select(p => p.Id).ToHashSet();

            var syncedCount = 0;

            // Map and save each product
            foreach (var dto in productsResponse.Products)
            {
                // Skip if product already exists
                if (existingIds.Contains(dto.Id))
                {
                    continue;
                }

                var product = new Product
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    DiscountPercentage = dto.DiscountPercentage,
                    Rating = (double)dto.Rating,
                    Stock = dto.Stock,
                    Brand = dto.Brand,
                    Category = dto.Category,
                    Thumbnail = dto.Thumbnail,
                    Images = JsonSerializer.Serialize(dto.Images) // Convert list to JSON string
                };

                await _unitOfWork.Products.AddAsync(product);
                syncedCount++;
            }

            // Save all changes to database
            await _unitOfWork.SaveChangesAsync();

            // Always invalidate product cache after sync operation
            // This ensures cache is refreshed even if no new products were added
            _cache.Remove(AllProductsCacheKey);

            return syncedCount;
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Failed to fetch products from DummyJSON API: {ex.Message}", ex);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Failed to parse DummyJSON API response: {ex.Message}", ex);
        }
    }
}