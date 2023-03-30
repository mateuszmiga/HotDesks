using Data.EFCore.DbContext;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task Create(TEntity obj)
        {
            obj.CreatedAt= DateTime.Now;
            _context.Set<TEntity>().Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<string> includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProp in includes)
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id, List<string> includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includes != null)
            {
                foreach (var includeProp in includes)
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync(q => q.Id == id);
        }
        

        public async Task UpdateAsync(TEntity obj)
        {
            obj.UpdatedAt= DateTime.Now;            
            _context.Entry(obj).State= EntityState.Modified;
            
            await _context.SaveChangesAsync();
        }
    }
}
