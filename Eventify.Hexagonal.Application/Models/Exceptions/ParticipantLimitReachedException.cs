namespace Eventify.Hexagonal.Application.Models.Exceptions;

public class ParticipantLimitReachedException(string message) : Exception(message);