namespace Eventify.VerticalSlice.Slices.CreateNewEvent;

public interface IForCheckingEventExists
{
    Task<bool> Exists(string name);
}