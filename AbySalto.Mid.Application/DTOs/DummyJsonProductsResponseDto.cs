using System.Text.Json.Serialization;

namespace AbySalto.Mid.Application.DTOs;

/// <summary>
/// DTO for mapping the complete response from DummyJSON API
/// </summary>
public class DummyJsonProductsResponseDto
{
    [JsonPropertyName("products")]
    public List<DummyJsonProductDto> Products { get; set; } = new();

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("skip")]
    public int Skip { get; set; }

    [JsonPropertyName("limit")]
    public int Limit { get; set; }
}