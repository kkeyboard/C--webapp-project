using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static async Task InitilizeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("GameStoreContext");
        services.AddNpgsql<GameStoreContext>(connString)
                .AddScoped<IGamesRepository, EntityFrameworkGamesRepository>();

        return services;
    }
}