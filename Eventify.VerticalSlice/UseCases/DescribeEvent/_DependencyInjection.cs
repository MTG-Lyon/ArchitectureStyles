using Eventify.VerticalSlice.Infrastructure;

namespace Eventify.VerticalSlice.UseCases.DescribeEvent;

public static class DependencyInjection
{
    public static IServiceCollection RegisterDescribeEventUseCase(this IServiceCollection services)
    {
        services
            .AddTransient<UseCase>()
            .AddTransient<IEventRepository, SqlEventRepository>()
            ;
        
        return services;
    }
}