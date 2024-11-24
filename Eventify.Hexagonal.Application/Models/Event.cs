namespace Eventify.Hexagonal.Domain.Models;

public class Event
{
    private Event(Guid id, EventName name, string description = "")
    {
        Id = id;
        Name = name;
        Description = description;
    }
    
    public static Event Register(EventName name) => 
        new(Guid.NewGuid(), name);

    public static Event Rehydrate(Guid id, EventName name, string description) =>
        new(id, name, description);
    
    public Guid Id { get; set; }
    public EventName Name { get; }
    public string Description { get; private set; }

    public void Describe(string description) =>
        Description = description;
}