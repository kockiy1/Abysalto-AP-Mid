using AbySalto.Mid.Application.DTOs.Favorite;
using AbySalto.Mid.Application.Services.Interfaces;
using AbySalto.Mid.Domain.Interfaces;
using AutoMapper;

namespace AbySalto.Mid.Application.Services;

/// <summary>
/// Service for favorite product operations with business logic
/// </summary>
public class FavoriteProductService : IFavoriteProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FavoriteProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets all favorite products for a user
    /// </summary>
    public async Task<IEnumerable<FavoriteProductDto>> GetUserFavoritesAsync(string userId)
    {
        var favorites = await _unitOfWork.FavoriteProducts.GetUserFavoritesAsync(userId);
        return _mapper.Map<IEnumerable<FavoriteProductDto>>(favorites);
    }

    /// <summary>
    /// Checks if a product is in user's favorites
    /// </summary>
    public async Task<bool> IsFavoriteAsync(string userId, int productId)
    {
        return await _unitOfWork.FavoriteProducts.IsFavoriteAsync(userId, productId);
    }

    /// <summary>
    /// Adds a product to user's favorites
    /// </summary>
    public async Task<FavoriteProductDto> AddToFavoritesAsync(string userId, int productId)
    {
        // Validate product exists
        var product = await _unitOfWork.Products.GetByIdAsync(productId);
        if (product == null)
        {
            throw new InvalidOperationException($"Product with ID {productId} not found.");
        }

        // Check if already favorite
        var isFavorite = await _unitOfWork.FavoriteProducts.IsFavoriteAsync(userId, productId);
        if (!isFavorite)
        {
            // Add to favorites
            await _unitOfWork.FavoriteProducts.AddFavoriteAsync(userId, productId);
            await _unitOfWork.SaveChangesAsync();
        }

        // Return favorite details (create DTO from product entity)
        return new FavoriteProductDto
        {
            ProductId = product.Id,
            ProductTitle = product.Title,
            ProductThumbnail = product.Thumbnail,
            ProductPrice = product.Price,
            AddedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Removes a product from user's favorites
    /// </summary>
    public async Task RemoveFromFavoritesAsync(string userId, int productId)
    {
        // Check if favorite exists
        var isFavorite = await _unitOfWork.FavoriteProducts.IsFavoriteAsync(userId, productId);
        if (!isFavorite)
        {
            throw new InvalidOperationException($"Product with ID {productId} is not in favorites.");
        }

        // Remove from favorites
        await _unitOfWork.FavoriteProducts.RemoveFavoriteAsync(userId, productId);
        await _unitOfWork.SaveChangesAsync();
    }
}
