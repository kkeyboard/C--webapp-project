using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;

public interface IGamesRepository
{
    void CreateAsync(Game game);
    void DeleteAsync(int id);
    Game? GetAsync(int id);
    IEnumerable<Game> GetAllAsync();
    void UpdateAsync(Game updatedGame);
}
