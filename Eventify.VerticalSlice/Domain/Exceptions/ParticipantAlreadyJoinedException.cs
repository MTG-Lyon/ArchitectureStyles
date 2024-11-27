using Eventify.VerticalSlice.Domain.Exceptions.Base;

namespace Eventify.VerticalSlice.Domain.Exceptions;

public class ParticipantAlreadyJoinedException(string message) : Exception(message), IDomainException;