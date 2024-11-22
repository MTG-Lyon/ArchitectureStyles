namespace Eventify.Tests.Acceptance.Configuration;

public static class ObjectExtensions
{
    public static T? MapIfNotNull<T>(this object? obj, Func<object, T> map) =>
        obj is null ? default : map(obj);
}