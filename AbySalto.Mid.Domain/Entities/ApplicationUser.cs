using Microsoft.AspNetCore.Identity;

namespace AbySalto.Mid.Domain.Entities;

/// <summary>
/// Custom user entity extending IdentityUser with additional properties
/// </summary>
public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Basket? Basket { get; set; }
    public ICollection<FavoriteProduct> FavoriteProducts { get; set; } = new List<FavoriteProduct>();
}
