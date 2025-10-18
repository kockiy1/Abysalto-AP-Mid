using AbySalto.Mid.Application.DTOs.Product;
using AbySalto.Mid.Application.Services.Interfaces;
using AbySalto.Mid.Domain.Interfaces;
using AutoMapper;

namespace AbySalto.Mid.Application.Services;

/// <summary>
/// Service for product operations with business logic
/// </summary>
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets all products
    /// </summary>
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    /// <summary>
    /// Gets a product by ID
    /// </summary>
    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product == null)
        {
            return null;
        }

        return _mapper.Map<ProductDto>(product);
    }

    /// <summary>
    /// Searches products by title or description
    /// </summary>
    public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return await GetAllProductsAsync();
        }

        var products = await _unitOfWork.Products.SearchAsync(searchTerm);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    /// <summary>
    /// Gets products by category
    /// </summary>
    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
        {
            return await GetAllProductsAsync();
        }

        var products = await _unitOfWork.Products.GetByCategoryAsync(category);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}
