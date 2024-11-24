using Eventify.Hexagonal.Domain.DrivenPorts;
using Eventify.Hexagonal.Infrastructure.Adapters;
using Eventify.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Hexagonal.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services
            .AddTransient<IEventRepository, SqlEventRepository>()
            .RegisterDatabase()
            ;

        return services;
    }
}