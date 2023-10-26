using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Data;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Repositories;

public class EntityFrameworkGamesRepository : IGamesRepository
{
    // Define the instance
    private readonly GameStoreContext dbContext;

    public EntityFrameworkGamesRepository(GameStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void CreateAsync(Game game)
    {
        dbContext.Games.Add(game);
        dbContext.SaveChanges(); // Send it to database.
    }

    public void DeleteAsync(int id)
    {
        // Delete without loading it to the memory.
        dbContext.Games.Where(game => game.Id == id).ExecuteDelete();
    }

    public Game? GetAsync(int id)
    {
        return dbContext.Games.Find(id);
    }

    public IEnumerable<Game> GetAllAsync()
    {
        return dbContext.Games.AsNoTracking().ToList();
    }

    public void UpdateAsync(Game updatedGame)
    {
        dbContext.Update(updatedGame);
        dbContext.SaveChanges();
    }
}