namespace Eventify.VerticalSlice.Shared.Infrastructure;

public class EntityNotFoundException(string value) : Exception(value);