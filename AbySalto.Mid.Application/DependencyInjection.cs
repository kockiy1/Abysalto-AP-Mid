using AbySalto.Mid.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace AbySalto.Mid.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register AutoMapper with all profiles
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        // TODO: Add application services

        return services;
    }
}
