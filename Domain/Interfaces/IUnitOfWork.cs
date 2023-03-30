using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Owner> Owners { get; }
        IGenericRepository<Desk> Desks { get; }
        IGenericRepository<Room> Rooms { get; }
        Task CommitChanges();
    }
}
