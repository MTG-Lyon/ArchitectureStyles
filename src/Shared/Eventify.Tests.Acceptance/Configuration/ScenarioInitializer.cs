using Reqnroll;

namespace Eventify.Tests.Acceptance.Configuration;

[Binding]
public class ScenarioInitializer(TestApplication application)
{
    [BeforeScenario]
    public Task InitializeAsync() =>
        application.Initialize();
}