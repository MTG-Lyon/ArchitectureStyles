using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.Tests.Acceptance.Configuration;

public class HttpException(string path, ProblemDetails problemDetails)
    : Exception($"{(HttpStatusCode)problemDetails.Status!} on {path} : {BuildErrorMessage(problemDetails)}")
{
    public ProblemDetails ProblemDetails { get; } = problemDetails;

    public HttpStatusCode HttpStatusCode =>
        (HttpStatusCode)(ProblemDetails.Status ?? throw new InvalidOperationException("No status"));

    private static string BuildErrorMessage(ProblemDetails problemDetails)
    {
        var result = new StringBuilder();

        result.Append(problemDetails.Title ?? "No details");

        if (problemDetails.Extensions.TryGetValue("errors", out var errors))
        {
            var json = (JsonElement?)(errors ?? null);
            if (json is not null)
            {
                foreach (var validationError in json.Value.EnumerateObject().SelectMany(x => x.Value.EnumerateArray()))
                {
                    result.Append(" " + validationError.GetString());
                }
            }
        }

        return result.ToString();
    }

    public static Exception From(string path, HttpResponseMessage response)
        => new HttpException($"{response.RequestMessage?.Method} {path}",
            new ProblemDetails { Status = (int)response.StatusCode });

    public static Exception From(string path, ProblemDetails problemDetails)
        => new HttpException(path, problemDetails);
}