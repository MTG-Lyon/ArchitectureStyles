using System.Text.Json;
using Eventify.Tests.Acceptance.Configuration.TestServers;
using Reqnroll;

namespace Eventify.Tests.Acceptance.Configuration;

public class TestApplication(
    HexagonalTestServer hexagonalTestServer,
    CleanTestServer cleanTestServer,
    FeatureInfo featureInfo,
    ErrorDriver errorDriver
)
{
    private TestHttpClient? _client;
    private ITestServer? _testServer;

    public TestHttpClient Client => _client
        ??= new TestHttpClient(
            _testServer!.CreateClient(),
            errorDriver,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }
        );
    
    public Task Initialize()
    {
        _testServer = GetTestServer();
        return _testServer.Initialize();
    }

    private ITestServer GetTestServer()
    {
        if(featureInfo.Tags.Contains("Clean"))
        {
            return cleanTestServer;
        }

        if(featureInfo.Tags.Contains("Hexagonal"))
        {
            return hexagonalTestServer;
        }

        throw new InvalidOperationException("No test server found");
    }
}