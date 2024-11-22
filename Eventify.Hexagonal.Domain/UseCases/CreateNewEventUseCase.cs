using Eventify.Hexagonal.Domain.Models;
using Eventify.Hexagonal.Domain.Ports;
using Eventify.Hexagonal.Domain.Ports2;

namespace Eventify.Hexagonal.Domain.UseCases;

internal class CreateNewEventUseCase(
    IEventRepository eventRepository
) : ICreateNewEventUseCase
{
    public Task Execute(string name)
    {
        var @event = new Event(new EventName(name));

        return eventRepository.Save(@event);
    }
}