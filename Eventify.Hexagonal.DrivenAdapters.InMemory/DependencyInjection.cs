using Eventify.Hexagonal.Domain.DrivenPorts;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Hexagonal.DrivenAdapters.InMemory;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInMemoryDatabase(this IServiceCollection services)
    {
        services
            .AddSingleton<IEventRepository, InMemoryEventRepository>()
            ;
        
        return services;
    }
}