using Eventify.VerticalSlice.Shared.Domain;

namespace Eventify.VerticalSlice.Slices.CommentEvent;

public interface IEventRepository
{
    Task<Event> Get(Guid eventId);
    Task Save(Event @event);
}