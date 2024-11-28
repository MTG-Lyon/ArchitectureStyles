namespace Eventify.Clean.Domain;

public interface IEventRepository
{
    Task<bool> Exists(string name);
    Task<Event> Get(Guid eventId);
    Task<EventDetailsDto> GetDetails(Guid eventId);
    Task<IReadOnlyCollection<EventListItemDto>> GetAll();
    Task Save(Event @event);
}

public record EventDetailsDto(IReadOnlyCollection<CommentDto> Comments);

public record CommentDto(DateTime Date, string Commenter, string Comment);