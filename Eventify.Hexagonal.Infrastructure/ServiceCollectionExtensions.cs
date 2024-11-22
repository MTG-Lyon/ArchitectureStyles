using Eventify.Hexagonal.Domain.Ports2;
using Eventify.Hexagonal.Infrastructure.Adapters;
using Eventify.Hexagonal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Hexagonal.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services
            .AddTransient<IEventRepository, SqlEventRepository>()
            .AddDbContext<EventifyDbContext>((_, options) =>
            {
                options.UseNpgsql("");
            })
            ;

        return services;
    }
}