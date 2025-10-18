using System.Text.Json;
using AbySalto.Mid.Application.DTOs.Auth;
using AbySalto.Mid.Application.DTOs.Basket;
using AbySalto.Mid.Application.DTOs.Favorite;
using AbySalto.Mid.Application.DTOs.Product;
using AbySalto.Mid.Domain.Entities;
using AutoMapper;

namespace AbySalto.Mid.Application.Mappings;

/// <summary>
/// AutoMapper profile for entity to DTO mappings
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product mappings
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Images,
                opt => opt.MapFrom(src => ParseImages(src.Images)));

        // Basket mappings
        CreateMap<Basket, BasketDto>();
        CreateMap<BasketItem, BasketItemDto>();

        // Favorite mappings
        CreateMap<FavoriteProduct, FavoriteProductDto>()
            .ForMember(dest => dest.ProductTitle,
                opt => opt.MapFrom(src => src.Product.Title))
            .ForMember(dest => dest.ProductThumbnail,
                opt => opt.MapFrom(src => src.Product.Thumbnail))
            .ForMember(dest => dest.ProductPrice,
                opt => opt.MapFrom(src => src.Product.Price));

        // Auth mappings
        CreateMap<ApplicationUser, AuthResponseDto>()
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Token,
                opt => opt.Ignore()); // Token is set manually in service
    }

    /// <summary>
    /// Parses JSON string of images to List of strings
    /// </summary>
    private static List<string> ParseImages(string imagesJson)
    {
        if (string.IsNullOrWhiteSpace(imagesJson))
        {
            return new List<string>();
        }

        try
        {
            var images = JsonSerializer.Deserialize<List<string>>(imagesJson);
            return images ?? new List<string>();
        }
        catch
        {
            return new List<string>();
        }
    }
}
