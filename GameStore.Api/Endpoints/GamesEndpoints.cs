using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // app.MapGet("/games", () => games);
            group.MapGet("/", (IGamesRepository repository) => repository.GetAll());

            // Find the game where requested id matches with game id.

            // This code send 200 ok for non-existing request.
            // app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id));

            // Now it sends 404 error code.
            group.MapGet("/{id}", (IGamesRepository repository, int id) => 
            {
                Game? game = repository.Get(id);
                return game is not null ? Results.Ok(game) : Results.NotFound();
            })
            .WithName(GetGameEndpointName); // Provide the endpoint.

            // POST
            group.MapPost("/", (IGamesRepository repository, Game game) => 
            {
                repository.Create(game);
                return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
            });


            // PUT
            group.MapPut("/{id}", (IGamesRepository repository, int id, Game updatedGame) => 
            {
                Game? existingGame = repository.Get(id);

                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                existingGame.Name = updatedGame.Name;
                existingGame.Genre = updatedGame.Genre;
                existingGame.Price = updatedGame.Price;
                existingGame.ReleaseDate = updatedGame.ReleaseDate;
                existingGame.ImageUri = updatedGame.ImageUri;

                repository.Update(existingGame);

                return Results.NoContent();
            });


            // DELETE
            group.MapDelete("/{id}", (IGamesRepository repository, int id) => 
            {
                Game? game = repository.Get(id);

                if (game is not null)
                {
                    repository.Delete(id);
                }

                return Results.NoContent();
            });

            return group;
        }
    }
}