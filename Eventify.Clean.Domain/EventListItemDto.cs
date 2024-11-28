namespace Eventify.Clean.Domain;

public record EventListItemDto(
    Guid Id,
    string Name,
    string Description,
    EventStatus Status,
    IReadOnlyCollection<ParticipantDto> Participants
);

public record ParticipantDto(string EmailAddress);