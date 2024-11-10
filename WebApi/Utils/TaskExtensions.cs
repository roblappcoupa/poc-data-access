namespace WebApi.Utils;

internal static class TaskExtensions
{
    public static Task<T> WrapInTask<T>(this T obj) => Task.FromResult(obj);
}