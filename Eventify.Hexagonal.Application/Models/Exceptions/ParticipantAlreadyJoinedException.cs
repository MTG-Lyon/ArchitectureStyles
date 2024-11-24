namespace Eventify.Hexagonal.Domain.Models;

public class ParticipantAlreadyJoinedException(string message) : Exception(message);