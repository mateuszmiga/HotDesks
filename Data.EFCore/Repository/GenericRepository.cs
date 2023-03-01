using Data.EFCore.DbContext;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EFCore.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly Context _context;

        public GenericRepository(Context context)
        {
            _context = context;
        }

        public void Create(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public void Delete(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll() => _context.Set<TEntity>().ToList();

        public TEntity GetById(int id) => _context.Set<TEntity>().SingleOrDefault(e => e.Id == id);
        

        public void Update(int id, TEntity obj)
        {
            var entity = GetById(id);

            _context.Set<TEntity>().Remove(entity);
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
