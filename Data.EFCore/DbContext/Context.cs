﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EFCore.DbContext
{
    public class Context : Microsoft.EntityFrameworkCore.DbContext
    {
        
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        
        public DbSet<Desk> Desks { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Room> Rooms { get; set; }

       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Desk>().HasKey(d => d.Id);
            builder.Entity<Desk>().HasOne(o => o.Owner).WithOne(d => d.Desk);
            builder.Entity<Desk>().HasOne(o => o.Room).WithMany(d => d.Desks);
            builder.Entity<Desk>().Property(p => p.Description).HasMaxLength(128);
            builder.Entity<Desk>().Property(p => p.RentingStart).HasColumnType("date");
            builder.Entity<Desk>().Property(p => p.RentingEnd).HasColumnType("date");
        }

    }
}
