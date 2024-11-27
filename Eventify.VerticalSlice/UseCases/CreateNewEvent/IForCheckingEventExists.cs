namespace Eventify.VerticalSlice.UseCases.CreateNewEvent;

public interface IForCheckingEventExists
{
    Task<bool> Exists(string name);
}