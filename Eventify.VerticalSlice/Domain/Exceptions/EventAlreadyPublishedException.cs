using Eventify.VerticalSlice.Domain.Exceptions.Base;

namespace Eventify.VerticalSlice.Domain.Exceptions;

public class EventAlreadyPublishedException(string message) : Exception(message), IDomainException;