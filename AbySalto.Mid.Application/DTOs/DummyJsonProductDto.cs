using System.Text.Json.Serialization;

namespace AbySalto.Mid.Application.DTOs;

/// <summary>
/// DTO for mapping product data from DummyJSON API response
/// </summary>
public class DummyJsonProductDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("discountPercentage")]
    public decimal DiscountPercentage { get; set; }

    [JsonPropertyName("rating")]
    public decimal Rating { get; set; }

    [JsonPropertyName("stock")]
    public int Stock { get; set; }

    [JsonPropertyName("brand")]
    public string Brand { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("thumbnail")]
    public string Thumbnail { get; set; } = string.Empty;

    [JsonPropertyName("images")]
    public List<string> Images { get; set; } = new();
}