using GameStore.Api.Entities;

namespace GameStore.Api.Repositories
{
    public interface IGamerepository
    {
        void Create(Game game);
        void Delete(int id);
        Game? Get(int id);
        IEnumerable<Game> GetAll();
        void Update(Game game);
    }
}