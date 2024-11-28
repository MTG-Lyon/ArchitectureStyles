using Eventify.VerticalSlice.Slices.CommentEvent;
using Eventify.VerticalSlice.Slices.CreateNewEvent;
using Eventify.VerticalSlice.Slices.DescribeEvent;
using Eventify.VerticalSlice.Slices.GetEventDetails;
using Eventify.VerticalSlice.Slices.JoinEvent;
using Eventify.VerticalSlice.Slices.ListExistingEvents;
using Eventify.VerticalSlice.Slices.PublishEvent;

namespace Eventify.VerticalSlice.Slices;

public static class DependencyInjection
{
    public static IServiceCollection RegisterUseCases(this IServiceCollection services)
    {
        services
            .RegisterCreateNewEventUseCase()
            .RegisterListExistingEventsUseCase()
            .RegisterDescribeEventUseCase()
            .RegisterPublishEventUseCase()
            .RegisterJoinEventUseCase()
            .RegisterCommentEventUseCase()
            .RegisterGetEventDetailsUseCase()
            ;
        
        return services;
    }
}