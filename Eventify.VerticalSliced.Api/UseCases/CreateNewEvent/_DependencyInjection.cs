using Eventify.VerticalSliced.Api.Infrastructure;

namespace Eventify.VerticalSliced.Api.UseCases.CreateNewEvent;

public static class DependencyInjection
{
    public static IServiceCollection RegisterCreateNewEventUseCase(this IServiceCollection services)
    {
        services
            .AddTransient<UseCase>()
            .AddTransient<IEventRepository, SqlEventRepository>()
            ;
        
        return services;
    }
}