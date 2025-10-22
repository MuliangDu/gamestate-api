namespace GameStore.Api.DTOs;

public record class CreateGameDtos(
    string Title,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
