using Data.EFCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EFCore.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly Context _context;

        public GenericRepository(Context context)
        {
            _context = context;
        }

        public void Create(TEntity obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
        }

        public IEnumerable<TEntity> GetAll() => _context.Set<TEntity>().ToList();

        public TEntity GetById(int id) => _context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
        

        public void Update(int id, TEntity obj)
        {
            _context.Set<TEntity>().SingleOrDefault;
        }
    }
}
