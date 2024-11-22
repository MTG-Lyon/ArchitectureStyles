namespace Eventify.Clean.Domain;

public interface IEventRepository
{
    Task Save(Event @event);
    Task<IReadOnlyCollection<EventListItemDto>> GetAll();
}