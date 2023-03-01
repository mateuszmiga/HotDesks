using Domain.Entities;

namespace Data.EFCore.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public void Create(TEntity obj);

        public void Delete(TEntity entity);

        public IEnumerable<TEntity> GetAll();

        public TEntity GetById(int id);


        public void Update(int id, TEntity obj);
        
    }
}