namespace Eventify.VerticalSlice.UseCases.DescribeEvent;

public class UseCase(IEventRepository eventRepository)
{
    public async Task Describe(Guid eventId, string description)
    {
        var @event = await eventRepository.Get(eventId);

        @event.Describe(description);

        await eventRepository.Save(@event);
    }
}