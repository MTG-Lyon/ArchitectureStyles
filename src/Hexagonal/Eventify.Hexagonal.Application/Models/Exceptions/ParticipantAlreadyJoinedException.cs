using Eventify.Hexagonal.Application.Models.Exceptions.Base;

namespace Eventify.Hexagonal.Application.Models.Exceptions;

public class ParticipantAlreadyJoinedException(string message) : Exception(message), IDomainException;