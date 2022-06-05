using ChessMate.Infrastructure.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ChessMate.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity:BaseEntity
    {
        Task<TEntity> GetAsync(int id);

        Task<int> CreateAsync(TEntity entity);

        IQueryable<TEntity> Table { get; }
    }
}
