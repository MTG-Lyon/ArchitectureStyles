using Eventify.VerticalSliced.Api.Infrastructure;

namespace Eventify.VerticalSliced.Api.UseCases.ListExistingEvents;

public static class DependencyInjection
{
    public static IServiceCollection RegisterListExistingEventsUseCase(this IServiceCollection services)
    {
        services
            .AddTransient<UseCase>()
            .AddTransient<IEventRepository, SqlEventRepository>()
            ;
        
        return services;
    }
}