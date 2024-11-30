namespace Eventify.VerticalSlice.Slices.PublishEvent;

public class UseCase(IEventRepository eventRepository)
{
    public async Task Publish(Guid eventId)
    {
        var @event = await eventRepository.Get(eventId);

        @event.Publish();

        await eventRepository.Save(@event);
    }
}