using Eventify.VerticalSlice.Infrastructure;

namespace Eventify.VerticalSlice.UseCases.CreateNewEvent;

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