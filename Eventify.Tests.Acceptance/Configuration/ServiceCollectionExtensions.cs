using Eventify.Hexagonal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Tests.Acceptance.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ReplaceWithInMemoryDatabase(this IServiceCollection services, string databaseName)
    {
        return services
            .AddScoped(_ => new DbContextOptionsBuilder<EventifyDbContext>()
                .UseInMemoryDatabase(databaseName).Options
            );
    }
}