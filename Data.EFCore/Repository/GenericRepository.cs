using Data.EFCore.DbContext;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<TEntity>> GetAll() => await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(int id) => await _context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
        

        public async Task UpdateAsync(TEntity obj)
        {
            obj.UpdatedAt= DateTime.Now;            
            _context.Entry(obj).State= EntityState.Modified;
            
            await _context.SaveChangesAsync();
        }
    }
}
