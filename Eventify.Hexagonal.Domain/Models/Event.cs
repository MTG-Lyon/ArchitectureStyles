namespace Eventify.Hexagonal.Domain.Models;

public class Event(EventName name)
{
    public EventName Name { get; } = name;
}