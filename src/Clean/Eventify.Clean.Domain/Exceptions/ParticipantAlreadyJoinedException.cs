using Eventify.Clean.Domain.Exceptions.Base;

namespace Eventify.Clean.Domain.Exceptions;

public class ParticipantAlreadyJoinedException(string message) : Exception(message), IDomainException;