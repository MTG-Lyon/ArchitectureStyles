using Eventify.Clean.Domain;

namespace Eventify.Clean.Application.Events.Administration;

public class DescribeEventUseCase(IEventRepository eventRepository)
{
    public async Task Describe(Guid eventId, string description)
    {
        var @event = await eventRepository.Get(eventId);

        @event.Describe(description);

        await eventRepository.Save(@event);
    }
}