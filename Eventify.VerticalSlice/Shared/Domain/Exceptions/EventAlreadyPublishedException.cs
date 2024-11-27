using Eventify.VerticalSlice.Shared.Domain.Exceptions.Base;

namespace Eventify.VerticalSlice.Shared.Domain.Exceptions;

public class EventAlreadyPublishedException(string message) : Exception(message), IDomainException;