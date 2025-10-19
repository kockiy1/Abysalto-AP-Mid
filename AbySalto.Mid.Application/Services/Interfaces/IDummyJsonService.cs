namespace AbySalto.Mid.Application.Services.Interfaces;

/// <summary>
/// Service for integrating with DummyJSON API
/// </summary>
public interface IDummyJsonService
{
    /// <summary>
    /// Fetches products from DummyJSON API and syncs them to the database
    /// </summary>
    /// <param name="limit">Maximum number of products to fetch (default: 100)</param>
    /// <returns>Number of products successfully synced</returns>
    Task<int> SyncProductsAsync(int limit = 100);
}