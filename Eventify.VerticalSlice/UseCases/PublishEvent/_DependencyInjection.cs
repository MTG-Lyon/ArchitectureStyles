using Eventify.VerticalSlice.Infrastructure;

namespace Eventify.VerticalSlice.UseCases.PublishEvent;

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