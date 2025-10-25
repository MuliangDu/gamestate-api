using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTOs;

public record class UpdateGameDtos(
    [Required][StringLength(50)] string Title,
    [Required][StringLength(20)] string Genre,
    [Range(0,100)] decimal Price,
    DateOnly ReleaseDate
);
