namespace GameStore.Api.DTOs;

public record class GameDto(
    int Id,
    string Title,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);
