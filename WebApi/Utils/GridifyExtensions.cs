namespace WebApi.Utils;

using Gridify;
using WebApi.Models;

internal static class GridifyExtensions
{
    public static GridifyQuery ToGridifyQuery(this SearchParams searchParams)
        => new(
            searchParams.Page,
            searchParams.PageSize,
            searchParams.Filter,
            searchParams.OrderBy);
}