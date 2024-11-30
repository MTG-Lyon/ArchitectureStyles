using Eventify.Clean.Domain.Exceptions.Base;

namespace Eventify.Clean.Domain.Exceptions;

public class EventAlreadyPublishedException(string message) : Exception(message), IDomainException;