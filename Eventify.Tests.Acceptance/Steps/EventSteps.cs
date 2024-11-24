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
        var events = await ListEvents();
        
        var eventId = events!
            .First(x => x.Name == eventName)
            .Id;
        
        await DescribeEvent(eventId, description);
    }

    [Then(@"the event list is")]
    public async Task ThenTheEventListIs(Table table)
    {
        var events = await ListEvents();
     
        events!
            .CollectionShouldBeEquivalentToTable(table)
            .WithProperty(x => x.Name)
            .WithProperty(x => x.Description)
            .Assert();
    }

    private Task<IReadOnlyCollection<EventListItemTestDto>?> ListEvents() =>
        application.Client.Get<IReadOnlyCollection<EventListItemTestDto>>("/events");

    private Task DescribeEvent(Guid eventId, string description) =>
        application.Client.Post($"/events/{eventId}/description", new { description });
}

public record EventListItemTestDto(Guid Id, string Name, string Description);