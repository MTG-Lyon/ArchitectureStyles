using Eventify.VerticalSlice.Shared.Infrastructure;

namespace Eventify.VerticalSlice.Slices.CommentEvent;

public static class DependencyInjection
{
    public static IServiceCollection RegisterCommentEventUseCase(this IServiceCollection services)
    {
        services
            .AddTransient<UseCase>()
            .AddTransient<IEventRepository, SqlEventRepository>()
            ;
        
        return services;
    }
}