using System;
using GameStore.Api.DTOs;

namespace GameStore.Api.Endpoints;

// static class to hold game endpoints, wrapped in an extension method
public static class GamesEndpoints
{
    private const string GetGameEndpointName = "GetName";
    private static readonly List<GameDto> games =
    [
        new GameDto(1, "The Witcher 3: Wild Hunt", "RPG", 39.99m, new DateOnly(2015, 5, 19)),
        new GameDto(2, "Cyberpunk 2077", "Action RPG", 59.99m, new DateOnly(2020, 12, 10)),
        new GameDto(3, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18)),
        new GameDto(4, "Among Us", "Party", 4.99m, new DateOnly(2018, 6, 15)),
        new GameDto(5, "Hades", "Roguelike", 24.99m, new DateOnly(2020, 9, 17))
    ];

    // extension method to map game endpoints
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");

        // Endpoint to get all games
        app.MapGet("games", () => games);

        // Endpoint to get a game by ID
        group.MapGet("/{id:int}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndpointName);

        // Endpoint to create a new game
        group.MapPost("/", (CreateGameDtos createGameDto) =>
        {
            var newGame = new GameDto(
                Id: games.Count + 1,
                Title: createGameDto.Title,
                Genre: createGameDto.Genre,
                Price: createGameDto.Price,
                ReleaseDate: createGameDto.ReleaseDate
            );

            games.Add(newGame);
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = newGame.Id }, newGame);
        })
        .WithParameterValidation();

        group.MapPut("/{id:int}", (int id, UpdateGameDtos updateGameDto) =>
        {
            var existingGameId = games.FindIndex(game => game.Id == id);
            if (existingGameId == -1)
            {
                return Results.NotFound();
            }
            games[existingGameId] = new GameDto(
                Id: id,
                Title: updateGameDto.Title,
                Genre: updateGameDto.Genre,
                Price: updateGameDto.Price,
                ReleaseDate: updateGameDto.ReleaseDate
            );
            return Results.NoContent();
        }
        ).WithParameterValidation();

        group.MapDelete("/{id:int}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        }
        );
        return group;
    }
}
