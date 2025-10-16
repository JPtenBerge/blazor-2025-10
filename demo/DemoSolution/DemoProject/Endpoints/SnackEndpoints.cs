using System.Runtime.CompilerServices;
using Demo.Shared.Dtos;
using Demo.Shared.Entities;
using Demo.Shared.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Endpoints;

public static class SnackEndpoints
{
    public static void MapSnackEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/snacks");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:int}", Get);
        group.MapPost("/", Post);
        group.MapPut("/{id:int}", Put);
    }

    public static async Task<SnackGetAllResponseDto> GetAll(ISnackRepository snackRepository)
    {
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

    public static async Task Put()
    {
    }
}