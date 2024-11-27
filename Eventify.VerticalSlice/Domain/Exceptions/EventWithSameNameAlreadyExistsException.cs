using Eventify.VerticalSlice.Domain.Exceptions.Base;

namespace Eventify.VerticalSlice.Domain.Exceptions;

public class EventWithSameNameAlreadyExistsException(string message) : Exception(message), IDomainException;