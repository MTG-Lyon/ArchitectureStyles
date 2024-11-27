using Eventify.VerticalSlice.Infrastructure;

namespace Eventify.VerticalSlice.UseCases.CommentEvent;

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