using Eventify.VerticalSlice.Shared.Domain;

namespace Eventify.VerticalSlice.UseCases.CreateNewEvent;

public class UseCase(IForCheckingEventExists checking, IEventRepository repository)
{
    public async Task CreateNewEvent(string name)
    {
        if (await checking.Exists(name))
        {
            throw new EventWithSameNameAlreadyExistsException("The event name is already taken");
        }
        
        var @event = Event.Register(new EventName(name));
        
        await repository.Save(@event);
    }
}