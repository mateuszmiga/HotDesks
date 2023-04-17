﻿// <auto-generated />
using System;
using Data.EFCore.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.EFCore.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("Domain.Entities.Desk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("RentingEnd")
                        .HasColumnType("date");

                    b.Property<DateTime?>("RentingStart")
                        .HasColumnType("date");

                    b.Property<int>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId")
                        .IsUnique();

                    b.HasIndex("RoomId");

                    b.ToTable("Desks");
                });

            modelBuilder.Entity("Domain.Entities.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("Domain.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Domain.Entities.Desk", b =>
                {
                    b.HasOne("Domain.Entities.Owner", "Owner")
                        .WithOne("Desk")
                        .HasForeignKey("Domain.Entities.Desk", "OwnerId");

                    b.HasOne("Domain.Entities.Room", "Room")
                        .WithMany("Desks")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Domain.Entities.Owner", b =>
                {
                    b.Navigation("Desk")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Room", b =>
                {
                    b.Navigation("Desks");
                });
#pragma warning restore 612, 618
        }
    }
}
