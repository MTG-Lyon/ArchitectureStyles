namespace Eventify.VerticalSlice.UseCases.GetEventDetails;

public record EventDetailsDto(IReadOnlyCollection<CommentDto> Comments);