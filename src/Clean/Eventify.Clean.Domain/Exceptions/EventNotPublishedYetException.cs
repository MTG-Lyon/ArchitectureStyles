using Eventify.Clean.Domain.Exceptions.Base;

namespace Eventify.Clean.Domain.Exceptions;

public class EventNotPublishedYetException(string message) : Exception(message), IDomainException;