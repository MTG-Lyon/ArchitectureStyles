using Eventify.Tests.Acceptance.Configuration;
using Reqnroll;
using Reqnroll.Extensions.FluentTableAsserter;

namespace Eventify.Tests.Acceptance.Steps;

[Binding]
public class EventSteps(TestApplication application)
{
    [When(@"I create a new event ""(.*)""")]
    public Task WhenICreateANewEvent(string name) =>
        application.Client.Post("/api/events", new { name });

    [Then(@"the event list is")]
    public async Task ThenTheEventListIs(Table table)
    {
        var events = await ListEvents();
     
        events!
            .CollectionShouldBeEquivalentToTable(table)
            .WithProperty(x => x.Name)
            .Assert();
    }

    private Task<IReadOnlyCollection<EventListItemDto>?> ListEvents() =>
        application.Client.Get<IReadOnlyCollection<EventListItemDto>>("/api/events");
}

public record EventListItemDto(string Name);