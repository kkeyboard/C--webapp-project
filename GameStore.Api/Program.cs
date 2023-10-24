using GameStore.Api.Endpoints;
using GameStore.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// AddScoped, AddTransient, AddSingleton
builder.Services.AddSingleton<IGamesRepository, InMemGamesRepository>();

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
