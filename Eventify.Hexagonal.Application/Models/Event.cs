namespace Eventify.Hexagonal.Domain.Models;

public class Event
{
    private Event(Guid id, EventName name, string description, EventStatus status)
    {
        Id = id;
        Name = name;
        Description = description;
        Status = status;
    }
    
    public static Event Register(EventName name) => 
        new(Guid.NewGuid(), name, string.Empty, EventStatus.Draft);

    public static Event Rehydrate(Guid id, EventName name, string description, EventStatus status) =>
        new(id, name, description, status);
    
    public Guid Id { get; set; }
    public EventName Name { get; }
    public string Description { get; private set; }
    public EventStatus Status { get; private set; }

    public void Describe(string description) =>
        Description = description;

    public void Publish()
    {
        if(Status == EventStatus.Published)
        {
            throw new EventAlreadyPublishedException("The event is already published");
        }
        Status = EventStatus.Published;
    }
}