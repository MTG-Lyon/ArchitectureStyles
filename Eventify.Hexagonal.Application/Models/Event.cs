using Eventify.Hexagonal.Application.Models.Exceptions;

namespace Eventify.Hexagonal.Application.Models;

public class Event
{
    private const int MaxParticipantCount = 10;
    
    private readonly List<Participant> _participants;

    private Event(
        Guid id,
        EventName name,
        string description,
        EventStatus status,
        IReadOnlyCollection<Participant> participants
    )
    {
        Id = id;
        Name = name;
        Description = description;
        Status = status;
        _participants = participants.ToList();
    }
    
    public Guid Id { get; set; }
    public EventName Name { get; }
    public string Description { get; private set; }
    public EventStatus Status { get; private set; }
    public IReadOnlyCollection<Participant> Participants => _participants;
    
    public static Event Register(EventName name) => 
        new(Guid.NewGuid(), name, string.Empty, EventStatus.Draft, new List<Participant>());

    public static Event Rehydrate(
        Guid id,
        EventName name,
        string description,
        EventStatus status,
        IReadOnlyCollection<Participant> participants
    ) =>
        new(id, name, description, status, participants);

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

    public void Join(Participant participant)
    {
        if(Status != EventStatus.Published)
        {
            throw new EventNotPublishedYetException("The event is not published yet");
        }
        
        if(_participants.Contains(participant))
        {
            throw new ParticipantAlreadyJoinedException("The participant has already joined");
        }
        
        if(_participants.Count == MaxParticipantCount)
        {
            throw new ParticipantLimitReachedException("The event has reached its maximum participant limit");
        }
        
        _participants.Add(participant);
    }
}