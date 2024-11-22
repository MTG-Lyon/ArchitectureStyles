using Eventify.VerticalSliced.Api.UseCases.CreateNewEvent;
using Eventify.VerticalSliced.Api.UseCases.ListExistingEvents;

namespace Eventify.VerticalSliced.Api.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection RegisterUserCases(this IServiceCollection services)
    {
        services
            .RegisterCreateNewEventUseCase()
            .RegisterListExistingEventsUseCase()
            ;
        
        return services;
    }
}