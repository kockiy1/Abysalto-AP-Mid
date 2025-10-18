namespace AbySalto.Mid.Application.DTOs.Basket;

/// <summary>
/// DTO for user basket with items
/// </summary>
public class BasketDto
{
    public int Id { get; set; }
    public List<BasketItemDto> Items { get; set; } = new();
    public decimal TotalPrice { get; set; }
    public int TotalItems { get; set; }
}
