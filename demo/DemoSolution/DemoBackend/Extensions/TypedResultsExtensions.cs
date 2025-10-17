using System.Reflection;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace DemoBackend.Extensions;

public sealed class UnauthorizedHttpResult<T> :
    IResult,
    IStatusCodeHttpResult,
    IValueHttpResult,
    IEndpointMetadataProvider
{
    public T? Value { get; }
    object? IValueHttpResult.Value => Value;
    public int? StatusCode => StatusCodes.Status401Unauthorized;

    public UnauthorizedHttpResult(T? value = default) => Value = value;

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

        // Only write a body if you provided one
        if (Value is not null)
        {
            // Let ASP.NET Core serialize using configured options
            await httpContext.Response.WriteAsJsonAsync(Value);
        }
    }

    // Make OpenAPI/endpoint metadata reflect 401 + T
    public static void PopulateMetadata(MethodInfo method, EndpointBuilder builder)
    {
        builder.Metadata.Add(new ProducesResponseTypeAttribute(typeof(T), StatusCodes.Status401Unauthorized));
    }
}

public static class TypedResultsExtensions
{
    public static UnauthorizedHttpResult<T> Unauthorized<T>(T? value = default) => new(value);
}