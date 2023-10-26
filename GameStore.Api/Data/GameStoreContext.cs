using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreContext: DbContext
{
    public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
    {
    }

    public DbSet<Game> Games => Set<Game>();

    // When a migration is created, the configuration must be applied.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}   