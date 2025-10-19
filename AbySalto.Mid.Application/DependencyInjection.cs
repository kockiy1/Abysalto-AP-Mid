using AbySalto.Mid.Application.Mappings;
using AbySalto.Mid.Application.Services;
using AbySalto.Mid.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AbySalto.Mid.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register AutoMapper with all profiles
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        // Register application services
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IFavoriteProductService, FavoriteProductService>();
        services.AddScoped<IAuthService, AuthService>();

        // Register DummyJsonService with HttpClient
        services.AddHttpClient<IDummyJsonService, DummyJsonService>();

        return services;
    }
}
