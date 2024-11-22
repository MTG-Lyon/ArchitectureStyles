using System.Collections;
using System.Web;

namespace Eventify.Tests.Acceptance.Configuration;

public static class UrlExtensions
{
    public static string AddQueryParameters(this string uri, IDictionary<string, object?>? queryParameters)
    {
        if (queryParameters is null || !queryParameters.Any())
        {
            return uri;
        }

        var parameters = queryParameters
            .SelectMany(x => x.Value is not string && x.Value is IEnumerable enumerable
                ? enumerable
                    .Enumerate()
                    .Select(item => new KeyValuePair<string, object?>(x.Key, item))
                    .ToArray()
                : new[] { x }
            )
            .Where(x => x.Value is not null)
            .Select(x => $"{x.Key}={x.Value}")
            .Join("&");

        return uri + HttpUtility.UrlPathEncode("?" + parameters);
    }
}