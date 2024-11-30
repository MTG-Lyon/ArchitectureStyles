using Eventify.VerticalSlice.Shared.Infrastructure;

namespace Eventify.VerticalSlice.Slices.CreateNewEvent;

public static class DependencyInjection
{
    public static IServiceCollection RegisterCreateNewEventUseCase(this IServiceCollection services)
    {
        services
            .AddTransient<UseCase>()
            .AddTransient<IEventRepository, SqlEventRepository>()
            .AddTransient<IForCheckingEventExists, ForCreatingEventRepository>()
            ;
        
        return services;
    }
}