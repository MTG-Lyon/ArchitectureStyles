using Eventify.Clean.Application.Events;
using Eventify.Clean.Application.Events.Administration;
using Eventify.Clean.Application.Events.Participation;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Clean.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services
            .AddTransient<RegisterEventUseCase>()
            .AddTransient<ListAllEventsUseCase>()
            .AddTransient<DescribeEventUseCase>()
            .AddTransient<PublishEventUseCase>()
            .AddTransient<JoinEventUseCase>()
            .AddTransient<CommentEventUseCase>()
            .AddTransient<GetEventDetailsUseCase>()
            ;
        
        return services;
    }
}