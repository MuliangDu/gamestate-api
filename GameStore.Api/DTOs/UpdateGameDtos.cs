namespace GameStore.Api.DTOs;

public record class UpdateGameDtos(
    string Title,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
