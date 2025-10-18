namespace AbySalto.Mid.Application.DTOs.Product;

/// <summary>
/// DTO for paginated product list
/// </summary>
public class ProductListDto
{
    public List<ProductDto> Products { get; set; } = new();
    public int Total { get; set; }
    public int Skip { get; set; }
    public int Limit { get; set; }
}
