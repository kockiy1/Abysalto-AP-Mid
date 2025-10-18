namespace AbySalto.Mid.Domain.Entities;

/// <summary>
/// Product entity cached from DummyJSON API
/// </summary>
public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal DiscountPercentage { get; set; }
    public double Rating { get; set; }
    public int Stock { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Thumbnail { get; set; } = string.Empty;
    public string Images { get; set; } = string.Empty;

    // Cache metadata
    public DateTime CachedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdated { get; set; }
}
