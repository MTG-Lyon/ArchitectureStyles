using Eventify.Infrastructure.Database.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Infrastructure.Database;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterDatabase(this IServiceCollection services)
    {
        services
            .AddDbContext<EventifyDbContext>((_, options) =>
            {
                options.UseNpgsql("tutu");
            })
            ;
        
        return services;
    }
}