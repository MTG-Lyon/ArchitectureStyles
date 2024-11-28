namespace Eventify.Tests.Acceptance.Configuration.TestServers;

public interface ITestServer
{
    Task Initialize();
    HttpClient CreateClient();
    void OverrideCurrentDate(DateTime now);
    DateTime GetCurrentDate();
    EmailTest GetLastSentEmailTo(string toEmail);
}

public record EmailTest(string Subject, string Content);