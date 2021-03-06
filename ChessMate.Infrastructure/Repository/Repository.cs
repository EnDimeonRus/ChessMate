using ChessMate.Infrastructure.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ChessMate.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        ChessMateDbContext _ctx;
        public Repository(ChessMateDbContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<TEntity> Table => _ctx.Set<TEntity>();

        public async Task<int> CreateAsync(TEntity entity)
        {
            var createdEntity = _ctx.Add(entity);
            await _ctx.SaveChangesAsync();
            return createdEntity.Entity.ID;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _ctx.FindAsync<TEntity>(id);
        }
    }
}
