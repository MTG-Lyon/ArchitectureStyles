using Eventify.VerticalSlice.Domain;

namespace Eventify.VerticalSlice.UseCases.CreateNewEvent;

public interface IEventRepository
{
    Task<Event> Get(Guid eventId);
    Task Save(Event @event);
    Task<bool> Exists(string bodyName);
}