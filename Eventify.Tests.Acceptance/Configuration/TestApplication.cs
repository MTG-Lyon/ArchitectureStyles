using System.Text.Json;

namespace Eventify.Tests.Acceptance.Configuration;

public class TestApplication(
    TestServer2 testServer2,
    ErrorDriver errorDriver
)
{
    private TestHttpClient? _client;
    
    public TestHttpClient Client => _client
        ??= new TestHttpClient(
            testServer2.CreateClient(),
            errorDriver,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }
        );
    
    public Task Initialize()
    {
        _ = testServer2.Server;
        
        return Task.CompletedTask;
    }
}