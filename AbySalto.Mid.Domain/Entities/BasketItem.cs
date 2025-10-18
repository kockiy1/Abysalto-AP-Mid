namespace AbySalto.Mid.Domain.Entities;

/// <summary>
/// Item in shopping basket - stores product snapshot at time of addition
/// </summary>
public class BasketItem
{
    public int Id { get; set; }
    public int BasketId { get; set; }
    public Basket Basket { get; set; } = null!;

    // Product snapshot
    public int ProductId { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public string ProductThumbnail { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
