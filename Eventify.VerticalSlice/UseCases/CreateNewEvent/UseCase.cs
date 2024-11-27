using Eventify.VerticalSlice.Domain;

namespace Eventify.VerticalSlice.UseCases.CreateNewEvent;

public class UseCase(IEventRepository repository)
{
    public async Task CreateNewEvent(string name)
    {
        if (await repository.Exists(name))
        {
            throw new EventWithSameNameAlreadyExistsException("The event name is already taken");
        }
        
        var @event = Event.Register(new EventName(name));
        
        await repository.Save(@event);
    }
}