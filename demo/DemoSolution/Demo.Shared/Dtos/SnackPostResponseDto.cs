using Demo.Shared.Entities;

namespace Demo.Shared.Dtos;

public record SnackPostResponseDto : SnackDto
{
}

public static class SnackPostResponseDtoExtensions
{
    public static SnackPostResponseDto ToPostDto(this Snack entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Rating = entity.Rating,
            PhotoUrl = entity.PhotoUrl
        };
    }

    public static Snack ToEntity(this SnackPostRequestDto dto)
    {
        return new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Rating = dto.Rating,
            PhotoUrl = dto.PhotoUrl
        };
    }
}