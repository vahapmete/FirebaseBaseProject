using Application.Services.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        
        services.AddScoped<IProductRepository, ProductRepository>();
        

        return services;
    }
}
