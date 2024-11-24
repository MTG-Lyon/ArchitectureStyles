namespace Eventify.Hexagonal.Domain.Models;

public class ParticipantLimitReachedException(string message) : Exception(message);