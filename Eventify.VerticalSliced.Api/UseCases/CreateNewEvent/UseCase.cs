using Eventify.VerticalSliced.Api.Domain;

namespace Eventify.VerticalSliced.Api.UseCases.CreateNewEvent;

public class UseCase(IEventRepository repository)
{
    public async Task Execute(string bodyName)
    {
        var @event = new Event(new EventName(bodyName));
        
        await repository.Save(@event);
    }
}