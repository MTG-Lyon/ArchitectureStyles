using Eventify.VerticalSlice.Shared.Infrastructure;

namespace Eventify.VerticalSlice.Slices.PublishEvent;

public static class DependencyInjection
{
    public static IServiceCollection RegisterPublishEventUseCase(this IServiceCollection services)
    {
        services
            .AddTransient<UseCase>()
            .AddTransient<IEventRepository, SqlEventRepository>()
            ;
        
        return services;
    }
}