using Microsoft.OpenApi.Models;

namespace AbySalto.Mid;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AbySalto Mid API", Version = "v1" });
        });

        return services;
    }
}
