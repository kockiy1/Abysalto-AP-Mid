namespace AbySalto.Mid.Application.DTOs.Basket;

/// <summary>
/// DTO for basket item
/// </summary>
public class BasketItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public string ProductThumbnail { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    /// <summary>
    /// Calculated subtotal (Price * Quantity)
    /// </summary>
    public decimal Subtotal => Price * Quantity;
}
