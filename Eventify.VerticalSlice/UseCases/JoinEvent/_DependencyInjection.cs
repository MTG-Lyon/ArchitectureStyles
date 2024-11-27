using Eventify.VerticalSlice.Shared.Infrastructure;

namespace Eventify.VerticalSlice.UseCases.JoinEvent;

public static class DependencyInjection
{
    public static IServiceCollection RegisterJoinEventUseCase(this IServiceCollection services)
    {
        services
            .AddTransient<UseCase>()
            .AddTransient<IEventRepository, SqlEventRepository>()
            ;
        
        return services;
    }
}