using Eventify.VerticalSlice.Domain.Exceptions.Base;

namespace Eventify.VerticalSlice.Domain.Exceptions;

public class ParticipantLimitReachedException(string message) : Exception(message), IDomainException;