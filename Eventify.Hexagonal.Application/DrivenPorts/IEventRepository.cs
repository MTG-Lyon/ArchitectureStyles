using Eventify.Hexagonal.Application.DrivingPorts;
using Eventify.Hexagonal.Application.Models;
using Eventify.Hexagonal.Application.UseCases;

namespace Eventify.Hexagonal.Application.DrivenPorts;

public interface IEventRepository
{
    Task<Event> Get(Guid eventId);
    Task Save(Event @event);
    Task<IReadOnlyCollection<EventListItemDto>> GetAll();
    Task<bool> Exists(string name);
    Task<EventDetailsDto> GetDetails(Guid eventId);
}