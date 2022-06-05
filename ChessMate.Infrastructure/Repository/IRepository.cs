using ChessMate.Infrastructure.Models;
using System.Threading.Tasks;

namespace ChessMate.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity:BaseEntity
    {
        Task<TEntity> GetAsync(int id);

        Task<int> CreateAsync(TEntity entity);

    }
}
