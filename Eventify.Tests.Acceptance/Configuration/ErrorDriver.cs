using System.Net;
using FluentAssertions;
using Reqnroll;

namespace Eventify.Tests.Acceptance.Configuration;

public class ErrorDriver(ScenarioInfo scenarioInfo)
{
    private readonly Queue<Exception> _errors = new();

    public bool HasError => _errors.Any();

    public async Task<bool> TryExecute(Func<Task> action)
    {
        try
        {
            await action();
            return true;
        }
        catch (Exception exception)
        {
            if (!scenarioInfo.Tags.Contains("ErrorHandling"))
            {
                throw;
            }

            _errors.Enqueue(exception);
            return false;
        }
    }

    public async Task<TResult?> TryExecute<TResult>(Func<Task<TResult>> action)
    {
        try
        {
            return await action();
        }
        catch (Exception exception)
        {
            if (!scenarioInfo.Tags.Contains("ErrorHandling"))
            {
                throw;
            }

            _errors.Enqueue(exception);

            return default;
        }
    }

    public void AssertErrorOccurredWithMessage(string errorMessage)
    {
        _errors.Should().NotBeEmpty();

        var lastException = _errors.Dequeue();

        lastException.Should().NotBeNull();
        lastException.Message.Should().MatchRegex(errorMessage);
    }

    public void AssertNoErrorOccurred() => Assert.Empty(_errors);

    public void AssertHttpErrorOccurred(HttpStatusCode status, string type)
    {
        _errors.Should().NotBeEmpty();

        var lastException = _errors.Dequeue();

        lastException.Should().BeOfType<HttpException>();

        var httpException = (HttpException)lastException;
        ((HttpStatusCode)httpException.ProblemDetails.Status!).Should().Be(status);
        httpException.ProblemDetails.Type.Should().Be(type);
        // ((HttpException)lastException).ProblemDetails.Detail.Should().Be(detail);
    }

    public void AssertHttpErrorOccurred2(HttpStatusCode status, string errorMessage)
    {
        _errors.Should().NotBeEmpty();

        var lastException = _errors.Dequeue();

        lastException.Message.Should().MatchRegex(errorMessage);

        lastException.Should().BeOfType<HttpException>();

        var httpException = (HttpException)lastException;

        ((HttpStatusCode)httpException.ProblemDetails.Status!).Should().Be(status);
    }

    public HttpException GetLastError()
    {
        _errors.Should().NotBeEmpty("an exception must have occurred");

        var lastException = _errors.Dequeue();

        lastException.Should().BeOfType<HttpException>();

        return (HttpException)lastException;
    }

    public void Clear() =>
        _errors.Clear();
}