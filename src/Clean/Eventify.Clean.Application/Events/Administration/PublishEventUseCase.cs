namespace Eventify.Clean.Application.Events.Administration;

public class PublishEventUseCase(IEventRepository eventRepository)
{
    public async Task Publish(Guid eventId)
    {
        var @event = await eventRepository.Get(eventId);

        @event.Publish();

        await eventRepository.Save(@event);
    }
}