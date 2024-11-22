namespace Eventify.Clean.Domain;

public class Event(EventName name)
{
    public EventName Name { get; } = name;
}