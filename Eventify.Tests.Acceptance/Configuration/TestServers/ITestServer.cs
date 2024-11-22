namespace Eventify.Tests.Acceptance.Configuration.TestServers;

public interface ITestServer
{
    Task Initialize();
    HttpClient CreateClient();
}