namespace Eventify.Hexagonal.Application.Models.Exceptions;

public class ParticipantAlreadyJoinedException(string message) : Exception(message);