using Eventify.Tests.Acceptance.Configuration;
using Reqnroll;
using Reqnroll.Extensions.FluentTableAsserter;

namespace Eventify.Tests.Acceptance.Steps;

[Binding]
public class CommentSteps(TestApplication application, TestFinder finder)
{
    [Given(@"""(.*)"" has commented on the event ""(.*)"" with ""(.*)""")]
    [When(@"""(.*)"" comments on the event ""(.*)"" with ""(.*)""")]
    public async Task WhenCommentsOnTheEventWith(string commenter, string eventName, string comment)
    {
        var eventId = await finder.GetEventId(eventName);
        
        await application.Client.Post($"/events/{eventId}/comments", new { commenter, comment });
    }

    [Then(@"the event ""(.*)"" details contains the following comments")]
    public async Task ThenTheEventDetailsContainsTheFollowingComments(string eventName, Table table)
    {
        var eventId = await finder.GetEventId(eventName);
       
        var details = await application.Client.Get<EventDetailsTestDto>($"/events/{eventId}");
        
        details!.Comments
            .CollectionShouldBeEquivalentToTable(table)
            .WithProperty(x => x.Date)
            .WithProperty(x => x.Commenter)
            .WithProperty(x => x.Comment)
            .Assert();
    }
}

public record EventDetailsTestDto(IReadOnlyCollection<CommentTestDto> Comments);

public record CommentTestDto(DateTime Date, string Commenter, string Comment);