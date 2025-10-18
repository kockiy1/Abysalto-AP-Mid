using AbySalto.Mid.Application.DTOs.Basket;
using AbySalto.Mid.Application.Services.Interfaces;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Interfaces;
using AutoMapper;

namespace AbySalto.Mid.Application.Services;

/// <summary>
/// Service for basket operations with business logic
/// </summary>
public class BasketService : IBasketService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BasketService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets user's basket with all items
    /// </summary>
    public async Task<BasketDto?> GetUserBasketAsync(string userId)
    {
        var basket = await _unitOfWork.Baskets.GetByUserIdAsync(userId);

        if (basket == null)
        {
            return null;
        }

        return _mapper.Map<BasketDto>(basket);
    }

    /// <summary>
    /// Adds a product to user's basket
    /// </summary>
    public async Task<BasketDto> AddToBasketAsync(string userId, int productId, int quantity)
    {
        // Get or create basket
        var basket = await _unitOfWork.Baskets.GetByUserIdAsync(userId);

        if (basket == null)
        {
            // Create new basket for user
            basket = new Basket
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.Baskets.AddAsync(basket);
            await _unitOfWork.SaveChangesAsync();

            // Reload to get the ID
            basket = await _unitOfWork.Baskets.GetByUserIdAsync(userId);
        }

        // Validate product exists
        var product = await _unitOfWork.Products.GetByIdAsync(productId);
        if (product == null)
        {
            throw new InvalidOperationException($"Product with ID {productId} not found.");
        }

        // Check if product already exists in basket
        var existingItem = basket!.Items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem != null)
        {
            // Update existing item quantity
            await _unitOfWork.Baskets.UpdateItemQuantityAsync(existingItem.Id, existingItem.Quantity + quantity);
        }
        else
        {
            // Add new item to basket
            var basketItem = new BasketItem
            {
                BasketId = basket.Id,
                ProductId = productId,
                ProductTitle = product.Title,
                ProductThumbnail = product.Thumbnail,
                Price = product.Price,
                Quantity = quantity,
                AddedAt = DateTime.UtcNow
            };

            await _unitOfWork.Baskets.AddItemAsync(basketItem);
        }

        // Update basket timestamp
        basket.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.Baskets.UpdateAsync(basket);

        // Save all changes
        await _unitOfWork.SaveChangesAsync();

        // Return updated basket
        var updatedBasket = await GetUserBasketAsync(userId);
        return updatedBasket!;
    }

    /// <summary>
    /// Removes an item from user's basket
    /// </summary>
    public async Task<BasketDto> RemoveFromBasketAsync(string userId, int itemId)
    {
        var basket = await _unitOfWork.Baskets.GetByUserIdAsync(userId);

        if (basket == null)
        {
            throw new InvalidOperationException("Basket not found.");
        }

        // Validate item belongs to user's basket
        var item = basket.Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            throw new InvalidOperationException($"Item with ID {itemId} not found in basket.");
        }

        // Remove item
        await _unitOfWork.Baskets.RemoveItemAsync(itemId);

        // Update basket timestamp
        basket.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.Baskets.UpdateAsync(basket);

        // Save changes
        await _unitOfWork.SaveChangesAsync();

        // Return updated basket
        var updatedBasket = await GetUserBasketAsync(userId);
        return updatedBasket!;
    }

    /// <summary>
    /// Updates the quantity of a basket item
    /// </summary>
    public async Task<BasketDto> UpdateQuantityAsync(string userId, int itemId, int quantity)
    {
        if (quantity <= 0)
        {
            throw new InvalidOperationException("Quantity must be greater than 0.");
        }

        var basket = await _unitOfWork.Baskets.GetByUserIdAsync(userId);

        if (basket == null)
        {
            throw new InvalidOperationException("Basket not found.");
        }

        // Validate item belongs to user's basket
        var item = basket.Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            throw new InvalidOperationException($"Item with ID {itemId} not found in basket.");
        }

        // Update quantity
        await _unitOfWork.Baskets.UpdateItemQuantityAsync(itemId, quantity);

        // Update basket timestamp
        basket.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.Baskets.UpdateAsync(basket);

        // Save changes
        await _unitOfWork.SaveChangesAsync();

        // Return updated basket
        var updatedBasket = await GetUserBasketAsync(userId);
        return updatedBasket!;
    }

    /// <summary>
    /// Clears all items from user's basket
    /// </summary>
    public async Task ClearBasketAsync(string userId)
    {
        var basket = await _unitOfWork.Baskets.GetByUserIdAsync(userId);

        if (basket == null)
        {
            throw new InvalidOperationException("Basket not found.");
        }

        // Clear all items
        await _unitOfWork.Baskets.ClearBasketAsync(basket.Id);

        // Update basket timestamp
        basket.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.Baskets.UpdateAsync(basket);

        // Save changes
        await _unitOfWork.SaveChangesAsync();
    }
}
