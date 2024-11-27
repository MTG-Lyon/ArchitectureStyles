namespace Eventify.Clean.Entities;

public class Event(EventName name)
{
    public EventName Name { get; } = name;
}