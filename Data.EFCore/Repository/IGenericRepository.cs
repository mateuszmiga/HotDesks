using Domain.Entities;

namespace Data.EFCore.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public Task Create(TEntity obj);

        public Task Delete(TEntity entity);

        public Task<IEnumerable<TEntity>> GetAll();

        public Task<TEntity> GetByIdAsync(int id);

        public Task UpdateAsync(TEntity obj);
        
    }
}