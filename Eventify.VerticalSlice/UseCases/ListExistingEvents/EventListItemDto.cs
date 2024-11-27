namespace Eventify.VerticalSlice.UseCases.ListExistingEvents;

public record EventListItemDto(
    Guid Id,
    string Name,
    string Description,
    string Status,
    IReadOnlyCollection<ParticipantDto> Participants
);

public record ParticipantDto(string EmailAddress);