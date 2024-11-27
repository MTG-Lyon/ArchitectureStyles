using Eventify.VerticalSlice.Domain;

namespace Eventify.VerticalSlice.UseCases.DescribeEvent;

public interface IEventRepository
{
    Task<Event> Get(Guid eventId);
    Task Save(Event @event);
}