using Eventify.VerticalSliced.Api.Domain;

namespace Eventify.VerticalSliced.Api.UseCases.CreateNewEvent;

public interface IEventRepository
{
    Task Save(Event @event);
    Task<bool> Exists(string bodyName);
}