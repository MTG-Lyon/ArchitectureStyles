using Eventify.VerticalSlice.Domain;

namespace Eventify.VerticalSlice.UseCases.JoinEvent;

public class UseCase(IEventRepository eventRepository)
{
    public async Task JoinEvent(Guid eventId, string emailAddress)
    {
        var @event = await eventRepository.Get(eventId);

        @event.Join(new Participant(new EmailAddress(emailAddress)));

        await eventRepository.Save(@event);
    }

}