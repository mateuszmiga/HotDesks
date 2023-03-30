using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.EFCore.DbContext;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.EFCore.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;
        private IGenericRepository<Owner> _owners;
        private IGenericRepository<Desk> _desks;
        private IGenericRepository<Room> _rooms;

        public UnitOfWork(Context context)
        {
            _context = context;
        }

        public IGenericRepository<Owner> Owners => _owners ?? new GenericRepository<Owner>(_context);
        public IGenericRepository<Desk> Desks => _desks ?? new GenericRepository<Desk>(_context);
        public IGenericRepository<Room> Rooms => _rooms ?? new GenericRepository<Room>(_context);
        
        public async Task CommitChanges()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
