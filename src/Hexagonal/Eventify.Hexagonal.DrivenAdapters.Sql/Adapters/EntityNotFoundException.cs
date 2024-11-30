namespace Eventify.Hexagonal.DrivenAdapters.Sql.Adapters;

public class EntityNotFoundException(string message) : Exception(message);