using Eventify.Tests.Acceptance.Configuration;
using Reqnroll;
using Reqnroll.Extensions.FluentTableAsserter;

namespace Eventify.Tests.Acceptance.Steps;

[Binding]
public class EventSteps(TestApplication application)
{
    [Given(@"a new event ""(.*)"" created")]
    [When(@"I create a new event ""(.*)""")]
    public Task WhenICreateANewEvent(string name) =>
        application.Client.Post("/events", new { name });
    
    [When(@"I describe an unknown event")]
    public Task WhenIDescribeAnUnknownEvent() =>
        DescribeEvent(Guid.NewGuid(), "some description");
    
    [When(@"I describe the event ""(.*)"" with ""(.*)""")]
    public async Task WhenIDescribeTheEventWith(string eventName, string description)
    {
        var eventId = await GetEventId(eventName);

        await DescribeEvent(eventId, description);
    }

    [When(@"I publish the event ""(.*)""")]
    public async Task WhenIPublishTheEvent(string eventName)
    {
        var eventId = await GetEventId(eventName);
        
        await PublishEvent(eventId);
    }

    [Then(@"the event list is")]
    public async Task ThenTheEventListIs(Table table)
    {
        var events = await ListEvents();
     
        events!
            .CollectionShouldBeEquivalentToTable(table)
            .WithProperty(x => x.Name)
            .WithProperty(x => x.Description)
            .WithProperty(x => x.Status)
            .Assert();
    }

    private async Task<Guid> GetEventId(string eventName)
    {
        var events = await ListEvents();

        return events!
            .First(x => x.Name == eventName)
            .Id;
    }

    private Task<IReadOnlyCollection<EventListItemTestDto>?> ListEvents() =>
        application.Client.Get<IReadOnlyCollection<EventListItemTestDto>>("/events");

    private Task DescribeEvent(Guid eventId, string description) =>
        application.Client.Put($"/events/{eventId}/describe", new { description });

    private Task PublishEvent(Guid eventId) =>
        application.Client.Put($"/events/{eventId}/publish");
}

public record EventListItemTestDto(Guid Id, string Name, string Description, string Status);