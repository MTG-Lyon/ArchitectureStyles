namespace Eventify.Tests.Acceptance.Configuration.TestServers;

public interface ITestServer
{
    string Name { get; }
    Task Initialize();
    HttpClient CreateClient();
}