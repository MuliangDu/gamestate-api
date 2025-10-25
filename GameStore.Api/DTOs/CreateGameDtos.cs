using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTOs;

public record class CreateGameDtos(
    [Required][StringLength(50)] string Title,
    [Required][StringLength(20)] string Genre,
    [Range(0,100)] decimal Price,
    DateOnly ReleaseDate
);
