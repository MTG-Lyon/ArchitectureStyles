namespace Eventify.Tests.Acceptance.Configuration;

public static class StringExtensions
{
    public static string Join(this IEnumerable<string> values, string separator) =>
        string.Join(separator, values.ToArray());
}