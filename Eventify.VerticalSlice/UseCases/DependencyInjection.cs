using Eventify.VerticalSlice.UseCases.CreateNewEvent;
using Eventify.VerticalSlice.UseCases.DescribeEvent;
using Eventify.VerticalSlice.UseCases.JoinEvent;
using Eventify.VerticalSlice.UseCases.ListExistingEvents;
using Eventify.VerticalSlice.UseCases.PublishEvent;

namespace Eventify.VerticalSlice.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection RegisterUserCases(this IServiceCollection services)
    {
        services
            .RegisterCreateNewEventUseCase()
            .RegisterListExistingEventsUseCase()
            .RegisterDescribeEventUseCase()
            .RegisterPublishEventUseCase()
            .RegisterJoinEventUseCase()
            ;
        
        return services;
    }
}