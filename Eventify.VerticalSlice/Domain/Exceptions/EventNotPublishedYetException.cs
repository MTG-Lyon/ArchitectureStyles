using Eventify.VerticalSlice.Domain.Exceptions.Base;

namespace Eventify.VerticalSlice.Domain.Exceptions;

public class EventNotPublishedYetException(string message) : Exception(message), IDomainException;