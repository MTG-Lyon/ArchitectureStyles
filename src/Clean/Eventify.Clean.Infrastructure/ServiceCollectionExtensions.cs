using Eventify.Clean.Application;
using Eventify.Clean.Domain;
using Eventify.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Clean.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services
            .AddTransient<IEventRepository, SqlEventRepository>()
            .RegisterDatabase();
        
        return services;
    }
}