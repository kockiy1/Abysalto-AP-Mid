namespace AbySalto.Mid.Application.DTOs.Favorite;

/// <summary>
/// DTO for user's favorite product
/// </summary>
public class FavoriteProductDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public string ProductThumbnail { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public DateTime AddedAt { get; set; }
}
