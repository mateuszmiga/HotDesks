using Data.EFCore.Repository;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.EFCore.DbContext;
using Domain.Entities;

namespace Hotdesks.Tests
{
    internal class TestContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "TestDB");
            
        }

        public DbSet<Desk> Desks { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
