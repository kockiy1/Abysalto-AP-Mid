using AbySalto.Mid.Application.DTOs.Product;
using AbySalto.Mid.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.Controllers;

/// <summary>
/// Controller for product operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IDummyJsonService _dummyJsonService;

    public ProductController(IProductService productService, IDummyJsonService dummyJsonService)
    {
        _productService = productService;
        _dummyJsonService = dummyJsonService;
    }

    /// <summary>
    /// Gets all products
    /// </summary>
    /// <returns>List of all products</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    /// <summary>
    /// Gets a product by ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Product details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound(new { message = $"Product with ID {id} not found." });
        }

        return Ok(product);
    }

    /// <summary>
    /// Searches products by title or description
    /// </summary>
    /// <param name="term">Search term</param>
    /// <returns>List of matching products</returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> SearchProducts([FromQuery] string term)
    {
        var products = await _productService.SearchProductsAsync(term);
        return Ok(products);
    }

    /// <summary>
    /// Gets products by category
    /// </summary>
    /// <param name="category">Category name</param>
    /// <returns>List of products in the category</returns>
    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(string category)
    {
        var products = await _productService.GetProductsByCategoryAsync(category);
        return Ok(products);
    }

    /// <summary>
    /// Synchronizes products from DummyJSON API to the database
    /// </summary>
    /// <param name="limit">Maximum number of products to fetch (default: 100)</param>
    /// <returns>Number of products successfully synced</returns>
    [Authorize]
    [HttpPost("sync")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> SyncProducts([FromQuery] int limit = 100)
    {
        try
        {
            var syncedCount = await _dummyJsonService.SyncProductsAsync(limit);
            return Ok(new { message = $"Successfully synced {syncedCount} products from DummyJSON API.", syncedCount });
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }
}