using Eventify.VerticalSlice.Shared.Domain;

namespace Eventify.VerticalSlice.Slices.CreateNewEvent;

public interface IEventRepository
{
    Task Save(Event @event);
}