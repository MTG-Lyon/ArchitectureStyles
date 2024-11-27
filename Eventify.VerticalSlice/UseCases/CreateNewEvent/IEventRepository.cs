using Eventify.VerticalSlice.Shared.Domain;

namespace Eventify.VerticalSlice.UseCases.CreateNewEvent;

public interface IEventRepository
{
    Task<Event> Get(Guid eventId);
    Task Save(Event @event);
}