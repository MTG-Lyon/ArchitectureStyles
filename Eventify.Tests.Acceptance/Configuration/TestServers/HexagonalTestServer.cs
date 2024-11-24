using Eventify.Hexagonal.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reqnroll;

namespace Eventify.Tests.Acceptance.Configuration.TestServers;

public class HexagonalTestServer(
    IReqnrollOutputHelper outputHelper
) : WebApplicationFactory<Program>, ITestServer
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        var databaseName = Guid.NewGuid().ToString();

        builder
            .ConfigureLogging(x =>
            {
                x.AddSimpleConsole(option =>
                {
                    option.IncludeScopes = false;
                    option.TimestampFormat = "hh:mm:ss.ff ";
                });
                x.Services.AddSingleton<ILoggerProvider>(BuildDelegateLoggerProvider);
            })
            .ConfigureServices(services =>
            {
                services.ReplaceWithInMemoryDatabase(databaseName);
            })
            ;
    }

    private DelegateLoggerProvider BuildDelegateLoggerProvider(IServiceProvider serviceProvider)
    {
        var action = new Action<string>(outputHelper.WriteLine);
        return ActivatorUtilities.CreateInstance<DelegateLoggerProvider>(serviceProvider, action);
    }

    public string Name => "Hexagonal";

    public Task Initialize()
    {
        _ = Server;

        return Task.CompletedTask;
    }
}