namespace Eventify.VerticalSliced.Api.UseCases.ListExistingEvents;

public class UseCase(IEventRepository repository)
{
    public async Task<IReadOnlyCollection<EventListItemDto>> Execute() =>
        await repository.GetAll();
}

public record EventListItemDto(Guid Id, string Name);

public interface IEventRepository
{
    Task<IReadOnlyCollection<EventListItemDto>> GetAll();
}