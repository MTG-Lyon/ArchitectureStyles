using Eventify.Tests.Acceptance.Configuration;
using Reqnroll;

namespace Eventify.Tests.Acceptance.Steps;

[Binding]
public class ClockSteps(TestApplication application)
{
    [Given(@"the current date is (.*)")]
    public void GivenTheCurrentDateIs(DateTime now) =>
        application.Server.OverrideCurrentDate(now);

    [When(@"(.*) day passes")]
    public void WhenDayPasses(int dayCount)
    {
        var newDate = application.Server.GetCurrentDate().AddDays(dayCount);
        application.Server.OverrideCurrentDate(newDate);
    }
}