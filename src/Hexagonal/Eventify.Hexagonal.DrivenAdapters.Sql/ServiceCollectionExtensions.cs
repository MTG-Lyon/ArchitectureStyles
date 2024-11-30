using Eventify.Hexagonal.Application.DrivenPorts;
using Eventify.Hexagonal.DrivenAdapters.Sql.Adapters;
using Eventify.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Hexagonal.DrivenAdapters.Sql;

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