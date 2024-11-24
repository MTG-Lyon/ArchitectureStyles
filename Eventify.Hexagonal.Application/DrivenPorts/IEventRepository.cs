using Eventify.Hexagonal.Domain.Models;
using Eventify.Hexagonal.Domain.UseCases;

namespace Eventify.Hexagonal.Domain.DrivenPorts;

public interface IEventRepository
{
    Task<Event> Get(Guid eventId);
    Task Save(Event @event);
    Task<IReadOnlyCollection<EventListItemDto>> GetAll();
    Task<bool> Exists(string name);
}