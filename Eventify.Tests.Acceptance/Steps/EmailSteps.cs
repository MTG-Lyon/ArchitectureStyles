using Eventify.Tests.Acceptance.Configuration;
using FluentAssertions;
using Reqnroll;

namespace Eventify.Tests.Acceptance.Steps;

[Binding]
public class EmailSteps(TestApplication application)
{
    [Then(@"the following emails have been sent")]
    public void ThenTheFollowingEmailsHaveBeenSent(Table table)
    {
        var expected = table.CreateSet<(string EmailAddress, string Subject, string Content)>();

        foreach (var a in expected)
        {
            var email = application.Server.GetLastSentEmailTo(a.EmailAddress);

            email.Should().NotBeNull();
            email.Subject.Should().Be(a.Subject);
            email.Content.Should().Be(a.Content);
        }
    }
}