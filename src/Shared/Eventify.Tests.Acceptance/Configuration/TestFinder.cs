using Eventify.Tests.Acceptance.Steps;

namespace Eventify.Tests.Acceptance.Configuration;

public class TestFinder(TestApplication application)
{
    public async Task<Guid> GetEventId(string eventName)
    {
        var events = await application.Client.Get<IReadOnlyCollection<EventListItemTestDto>>("/events");
        
        return events!
            .Single(x => x.Name == eventName)
            .Id;
    }
}