using Eventify.Hexagonal.Application.DrivenPorts;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Hexagonal.DrivenAdapters.InMemory;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInMemoryDatabase(this IServiceCollection services)
    {
        services
            .AddSingleton<IEventRepository, InMemoryEventRepository>()
            .AddSingleton<IEmailSender, InMemoryEmailSender>()
            .AddSingleton<IClock, SystemClock>()
            ;
        
        return services;
    }
}