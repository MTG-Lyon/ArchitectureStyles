using Eventify.Clean.Domain;

namespace Eventify.Clean.Application.Events;

public class GetEventDetailsUseCase(IEventRepository eventRepository)
{
    public Task<EventDetailsDto> GetEventDetails(Guid eventId) =>
        eventRepository.GetDetails(eventId);
}