using Eventify.Clean.Application.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Clean.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services
            .AddTransient<CreateNewEventUseCase>()
            .AddTransient<ListAllEventsUseCase>()
            ;
        
        return services;
    }
}