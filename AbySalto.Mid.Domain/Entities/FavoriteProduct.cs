namespace AbySalto.Mid.Domain.Entities;

/// <summary>
/// Join table for user favorite products (many-to-many)
/// </summary>
public class FavoriteProduct
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
