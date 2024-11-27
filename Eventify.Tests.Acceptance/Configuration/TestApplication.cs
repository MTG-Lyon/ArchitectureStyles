using System.Text.Json;
using Eventify.Tests.Acceptance.Configuration.TestServers;

namespace Eventify.Tests.Acceptance.Configuration;

public class TestApplication(
    HexagonalTestServer hexagonalTestServer,
    CleanTestServer cleanTestServer,
    VerticalSliceTestServer verticalSliceTestServer,
    ErrorDriver errorDriver
)
{
    private const string TestServerName = "VerticalSlice";
    
    private TestHttpClient? _client;
    private ITestServer? _testServer;

    private IReadOnlyCollection<ITestServer> AllTestServers => [
        cleanTestServer, 
        hexagonalTestServer, 
        verticalSliceTestServer
    ];

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
        foreach (var testServer in AllTestServers)
        {
            if (testServer.Name == TestServerName)
            {
                return testServer;
            }
        }

        throw new InvalidOperationException("No test server found");
    }

}