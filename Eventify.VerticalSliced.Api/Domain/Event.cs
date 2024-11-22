namespace Eventify.VerticalSliced.Api.Domain;

public class Event(EventName name)
{
    public EventName Name { get; } = name;
}