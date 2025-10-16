using Demo.Shared.Entities;

namespace Demo.Shared.Dtos;

public record SnackPostRequestDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Rating { get; set; }
    public string PhotoUrl { get; set; } = null!;
}