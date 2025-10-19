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
        // Register base ProductService
        services.AddScoped<ProductService>();
        // Register cached decorator for IProductService
        services.AddScoped<IProductService>(provider =>
        {
            var productService = provider.GetRequiredService<ProductService>();
            var memoryCache = provider.GetRequiredService<Microsoft.Extensions.Caching.Memory.IMemoryCache>();
            return new CachedProductService(productService, memoryCache);
        });

        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IFavoriteProductService, FavoriteProductService>();
        services.AddScoped<IAuthService, AuthService>();

        // Register DummyJsonService with HttpClient
        services.AddHttpClient<IDummyJsonService, DummyJsonService>();

        return services;
    }
}
