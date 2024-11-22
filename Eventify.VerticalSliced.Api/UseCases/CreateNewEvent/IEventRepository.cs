using Eventify.VerticalSliced.Api.Domain;
using Eventify.VerticalSliced.Api.UseCases.ListExistingEvents;

namespace Eventify.VerticalSliced.Api.UseCases.CreateNewEvent;

public interface IEventRepository
{
    Task Save(Event @event);
}