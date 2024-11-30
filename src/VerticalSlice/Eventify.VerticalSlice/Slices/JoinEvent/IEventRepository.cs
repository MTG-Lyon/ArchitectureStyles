using Eventify.VerticalSlice.Shared.Domain;

namespace Eventify.VerticalSlice.Slices.JoinEvent;

public interface IEventRepository
{
    Task<Event> Get(Guid eventId);
    Task Save(Event @event);
}