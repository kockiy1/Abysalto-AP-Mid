using System.ComponentModel.DataAnnotations;

namespace AbySalto.Mid.Application.DTOs.Basket;

/// <summary>
/// DTO for adding a product to basket
/// </summary>
public class AddToBasketDto
{
    [Required]
    public int ProductId { get; set; }

    [Required]
    [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
    public int Quantity { get; set; } = 1;
}
