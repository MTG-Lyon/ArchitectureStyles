namespace Eventify.Tests.Acceptance.Configuration.TestServers;

public interface ITestServer
{
    string Name { get; }
    Task Initialize();
    HttpClient CreateClient();
    T GetService<T>() where T : notnull;
    void OverrideCurrentDate(DateTime now);
    DateTime GetCurrentDate();
    EmailTest GetLastSentEmailTo(string toEmail);
}

public record EmailTest(string Subject, string Content);