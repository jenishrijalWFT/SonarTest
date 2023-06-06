using Boss.Gateway.Application.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Boss.Gateway.Infrastructure;

public static class InfrastructureServiceRegistration
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICsvHelper, CsvHelper>();
        return services;
    }
}