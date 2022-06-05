using ChessMate.Infrastructure.Models;

namespace ChessMate.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity:BaseEntity
    {
        TEntity Get(int id);

        int Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);
    }
}
