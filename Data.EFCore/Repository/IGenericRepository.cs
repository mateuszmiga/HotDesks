using System.Linq.Expressions;
using Domain.Entities;

namespace Data.EFCore.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public Task Create(TEntity obj);

        public Task Delete(TEntity entity);

        public Task<IEnumerable<TEntity>> GetAll(
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<string> includes = null);

        public Task<TEntity> GetByIdAsync(int id, List<string> includes = null);

        public Task UpdateAsync(TEntity obj);
        
    }
}