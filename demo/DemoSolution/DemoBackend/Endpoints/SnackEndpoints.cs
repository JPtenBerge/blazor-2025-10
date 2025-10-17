using System.Security.Claims;
using Demo.Shared.Dtos;
using Demo.Shared.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DemoBackend.Endpoints;

public static class SnackEndpoints
{
    public static void MapSnackEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/snacks").RequireAuthorization("alleencoolemensen"); // [Authorize]
        // var group = app.MapGroup("api/snacks"); // [Authorize]

        group.MapGet("/", GetAll);
        group.MapGet("/{id:int}", Get);
        group.MapPost("/", Post);
        group.MapPut("/{id:int}", Put);
        group.MapDelete("/{id:int}", Delete);
    }

    public static async Task<SnackGetAllResponseDto> GetAll(ISnackRepository snackRepository, ClaimsPrincipal user)
    {
        Console.WriteLine($"Getting snacks voor {user.Identity?.Name}, is authed: {user.Identity?.IsAuthenticated}");
        var snacks = await snackRepository.GetAllAsync();
        return new() { Snacks = snacks.Select(s => s.ToDto()) };
    }

    public static async Task<Results<NotFound<string>, Ok<SnackDto>>> Get(ISnackRepository snackRepository, int id)
    {
        var snack = await snackRepository.GetAsync(id);
        return snack is null
            ? TypedResults.NotFound($"Could not find snack with id {id}")
            : TypedResults.Ok(snack.ToDto());
    }

    public static async Task<SnackPostResponseDto> Post(ISnackRepository snackRepository, SnackPostRequestDto newSnack)
    {
        return (await snackRepository.AddAsync(newSnack.ToEntity())).ToPostDto();
    }

    public static Task Put()
    {
        throw new NotImplementedException();
    }

    public static async Task<bool> Delete(IAuthorizationService authorizationService,ISnackRepository snackRepository, int id)
    {
        // authorizationService.AuthorizeAsync();
        return await snackRepository.DeleteAsync(id);
    }
}