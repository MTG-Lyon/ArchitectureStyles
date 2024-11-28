using Eventify.Clean.Domain.Exceptions.Base;

namespace Eventify.Clean.Domain.Exceptions;

public class EventWithSameNameAlreadyExistsException(string message) : Exception(message), IDomainException;