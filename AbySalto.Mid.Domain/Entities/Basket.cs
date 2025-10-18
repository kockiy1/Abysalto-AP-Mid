namespace AbySalto.Mid.Domain.Entities;

/// <summary>
/// Shopping basket - one basket per user (1:1 relationship)
/// </summary>
public class Basket
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
    public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Calculated properties
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    public int TotalItems => Items.Sum(item => item.Quantity);
}
