using Eventify.Hexagonal.Application.DrivingPorts;
using Eventify.Hexagonal.Application.Models;
using Eventify.Hexagonal.Application.UseCases;

namespace Eventify.Hexagonal.Application.DrivenPorts;

public interface IEventRepository
{
    Task<bool> Exists(string name);
    Task<Event> Get(Guid eventId);
    Task<EventDetailsDto> GetDetails(Guid eventId);
    Task<IReadOnlyCollection<EventListItemDto>> GetAll();
    Task Save(Event @event);
}