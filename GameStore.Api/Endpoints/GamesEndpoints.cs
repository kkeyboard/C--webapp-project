using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndpointName = "GetGame";

        public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
        {
            // Routing group
            var group = routes
                .MapGroup("/games")
                .WithParameterValidation();//Checks by MinimalApis.Extensions

            // GET
            group.MapGet("/", (IGamesRepository repository) => repository.GetAllAsync().Select(game => game.AsDto()));

            // Find the game where requested id matches with game id.

            // This code send 200 ok for non-existing request.
            // app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id));

            // Now it sends 404 error code.
            group.MapGet("/{id}", (IGamesRepository repository, int id) => 
            {
                Game? game = repository.GetAsync(id);
                return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();
            })
            .WithName(GetGameEndpointName); // Provide the endpoint.

            // POST
            group.MapPost("/", (IGamesRepository repository, CreateGameDto gameDto) => 
            {
                Game game = new()
                {
                    Name = gameDto.Name,
                    Genre = gameDto.Genre,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    ImageUri = gameDto.ImageUri
                };

                repository.CreateAsync(game);
                return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
            });


            // PUT
            group.MapPut("/{id}", (IGamesRepository repository, int id, UpdateGameDto updateGameDto) => 
            {
                Game? existingGame = repository.GetAsync(id);

                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                existingGame.Name = updateGameDto.Name;
                existingGame.Genre = updateGameDto.Genre;
                existingGame.Price = updateGameDto.Price;
                existingGame.ReleaseDate = updateGameDto.ReleaseDate;
                existingGame.ImageUri = updateGameDto.ImageUri;

                repository.UpdateAsync(existingGame);

                return Results.NoContent();
            });


            // DELETE
            group.MapDelete("/{id}", (IGamesRepository repository, int id) => 
            {
                Game? game = repository.GetAsync(id);

                if (game is not null)
                {
                    repository.DeleteAsync(id);
                }

                return Results.NoContent();
            });

            return group;
        }
    }
}