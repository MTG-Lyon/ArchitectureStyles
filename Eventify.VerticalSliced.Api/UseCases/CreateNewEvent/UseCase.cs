using Eventify.VerticalSliced.Api.Domain;

namespace Eventify.VerticalSliced.Api.UseCases.CreateNewEvent;

public class UseCase(IEventRepository repository)
{
    public async Task CreateNewEvent(string name)
    {
        if (await repository.Exists(name))
        {
            throw new EventWithSameNameAlreadyExistsException("The event name is already taken");
        }
        
        var @event = new Event(new EventName(name));
        
        await repository.Save(@event);
    }
}