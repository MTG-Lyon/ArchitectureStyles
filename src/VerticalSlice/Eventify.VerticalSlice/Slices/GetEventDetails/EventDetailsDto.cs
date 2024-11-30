namespace Eventify.VerticalSlice.Slices.GetEventDetails;

public record EventDetailsDto(IReadOnlyCollection<CommentDto> Comments);