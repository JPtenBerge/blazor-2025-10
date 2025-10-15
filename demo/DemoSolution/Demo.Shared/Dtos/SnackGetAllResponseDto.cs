using Demo.Shared.Entities;

namespace Demo.Shared.Dtos;

public record SnackGetAllResponseDto
{
    public required IEnumerable<SnackDto> Snacks { get; set; }

    // public int NrOfSnacksPerPage { get; set; }
    // public int CurrentPage { get; set; }
}

public record SnackDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Rating { get; set; }
    public required string PhotoUrl { get; set; }
}

public static class SnackDtoExtensions
{
    public static SnackDto ToDto(this Snack entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Rating = entity.Rating,
            PhotoUrl = entity.PhotoUrl
        };
    }

    public static Snack ToEntity(this SnackDto dto)
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