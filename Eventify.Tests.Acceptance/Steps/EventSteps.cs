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

    [Given(@"the event ""(.*)"" is published")]
    [When(@"I publish the event ""(.*)""")]
    public async Task WhenIPublishTheEvent(string eventName)
    {
        var eventId = await GetEventId(eventName);
        
        await PublishEvent(eventId);
    }
    
    [When(@"I join the event ""(.*)"" as ""(.*)""")]
    public async Task WhenIJoinTheEventAs(string eventName, string participantEmailAddress)
    {
        var eventId = await GetEventId(eventName);
        
        await JoinEvent(eventId, participantEmailAddress);
    }

    [Given(@"(.*) participants have joined the event ""(.*)""")]
    public async Task GivenParticipantsHaveJoinedTheEvent(int count, string eventName)
    {
        var eventId = await GetEventId(eventName);
        
        foreach (var _ in Enumerable.Range(0, count))
        {
            await JoinEvent(eventId, $"{Guid.NewGuid()}@example.com");
        }
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

    [Then(@"the ""(.*)"" event participant list is")]
    public async Task ThenTheEventParticipantListIs(string eventName,Table table)
    {
        var events = await ListEvents();
        
        var @event = events!.First(x => x.Name == eventName);
        
        @event.Participants
            .CollectionShouldBeEquivalentToTable(table)
            .WithProperty(x => x.EmailAddress)
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

    private Task JoinEvent(Guid eventId, string emailAddress) =>
        application.Client.Post($"/events/{eventId}/participants", new { emailAddress });
}

public record EventListItemTestDto(
    Guid Id,
    string Name,
    string Description,
    string Status,
    IReadOnlyCollection<ParticipantTestDto> Participants
);

public record ParticipantTestDto(string EmailAddress);