using System.Collections;

namespace Eventify.Tests.Acceptance.Configuration;

public static class EnumerableExtensions
{
    internal static IEnumerable<object?> Enumerate(this IEnumerable enumerable)
    {
        var enumerator = enumerable.GetEnumerator();
        while (enumerator.MoveNext()) yield return enumerator.Current;
    }
}