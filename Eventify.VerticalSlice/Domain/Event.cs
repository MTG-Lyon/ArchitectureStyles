using Eventify.VerticalSlice.Domain.Exceptions;

namespace Eventify.VerticalSlice.Domain;

public class Event
{
    private const int MaxParticipantCount = 10;
    
    private readonly List<Participant> _participants;
    private readonly List<EventComment> _comments;

    private Event(
        Guid id,
        EventName name,
        string description,
        EventStatus status,
        IReadOnlyCollection<Participant> participants,
        IReadOnlyCollection<EventComment> comments
    )
    {
        Id = id;
        Name = name;
        Description = description;
        Status = status;
        _participants = participants.ToList();
        _comments = comments.ToList();
    }
    
    public Guid Id { get; set; }
    public EventName Name { get; }
    public string Description { get; private set; }
    public EventStatus Status { get; private set; }
    public IReadOnlyCollection<Participant> Participants => _participants;
    public IReadOnlyCollection<EventComment> Comments => _comments;
    
    public static Event Register(EventName name) => 
        new(
            Guid.NewGuid(),
            name,
            string.Empty,
            EventStatus.Draft,
            new List<Participant>(),
            new List<EventComment>()
        );

    public static Event Rehydrate(
        Guid id,
        EventName name,
        string description,
        EventStatus status,
        IReadOnlyCollection<Participant> participants,
        IReadOnlyCollection<EventComment> comments
    ) =>
        new(id, name, description, status, participants, comments);

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

    public void AddComment(EventComment eventComment)
    {
        if(!_participants.Contains(eventComment.Commenter))
        {
            throw new CannotCommentEventException("A user who has not joined the event cannot comment on it");
        }
        
        _comments.Add(eventComment);
    }
}