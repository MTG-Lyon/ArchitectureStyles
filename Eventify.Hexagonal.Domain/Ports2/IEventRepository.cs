using Eventify.Hexagonal.Domain.Models;
using Eventify.Hexagonal.Domain.UseCases;

namespace Eventify.Hexagonal.Domain.Ports2;

public interface IEventRepository
{
    Task Save(Event @event);
    Task<IReadOnlyCollection<EventListItemDto>> GetAll();
}