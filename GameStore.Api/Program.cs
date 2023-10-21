using GameStore.Api.Entities;

// In-memory game lists
List<Game> games = new()
{
    new Game()
    {
        Id = 1,
        Name = "Street Fighter II",
        Genre = "Fighting",
        Price = 19.99M,
        ReleaseDate = new DateTime(1991, 2, 1),
        ImageUri = "https://placehold.co/100"
    },
    new Game()
    {
        Id = 2,
        Name = "Final Fantasy 14",
        Genre = "Roleplaying",
        Price = 59.99M,
        ReleaseDate = new DateTime(2010, 9, 30),
        ImageUri = "https://placehold.co/100"
    },
    new Game()
    {
        Id = 3,
        Name = "FIFA 23",
        Genre = "Sports",
        Price = 69.99M,
        ReleaseDate = new DateTime(2022, 9, 27),
        ImageUri = "https://placehold.co/100"
    },
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/games", () => games);

// Find the game where requested id matches with game id.

// This code send 200 ok for non-existing request.
// app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id));

app.MapGet("/games/{id}", (int id) => 
{
    Game? game = games.Find(game => game.Id == id);
    if (game is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(game);
});



app.Run();
