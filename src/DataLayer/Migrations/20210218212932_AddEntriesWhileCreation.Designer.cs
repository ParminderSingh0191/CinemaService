﻿// <auto-generated />
using System;
using CinemaService.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CinemaService.DataLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210218212932_AddEntriesWhileCreation")]
    partial class AddEntriesWhileCreation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CinemaService.DataLayer.Models.BookingDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CinemaShowId")
                        .HasColumnType("int");

                    b.Property<bool>("IsBooked")
                        .HasColumnType("bit");

                    b.Property<int?>("SeatId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CinemaShowId");

                    b.HasIndex("SeatId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("CinemaService.DataLayer.Models.CinemaShowDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("CinemaShows");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsAvailable = true,
                            Name = "The White Tiger"
                        },
                        new
                        {
                            Id = 2,
                            IsAvailable = true,
                            Name = "Spider man home coming"
                        },
                        new
                        {
                            Id = 3,
                            IsAvailable = true,
                            Name = "Avengers"
                        },
                        new
                        {
                            Id = 4,
                            IsAvailable = true,
                            Name = "Avengers Age of Ultron"
                        },
                        new
                        {
                            Id = 5,
                            IsAvailable = false,
                            Name = "Root"
                        });
                });

            modelBuilder.Entity("CinemaService.DataLayer.Models.SeatDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SeatNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Seats");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            SeatNumber = "A10"
                        },
                        new
                        {
                            Id = 2,
                            SeatNumber = "A11"
                        },
                        new
                        {
                            Id = 3,
                            SeatNumber = "A12"
                        },
                        new
                        {
                            Id = 4,
                            SeatNumber = "A13"
                        },
                        new
                        {
                            Id = 5,
                            SeatNumber = "A14"
                        },
                        new
                        {
                            Id = 6,
                            SeatNumber = "B10"
                        },
                        new
                        {
                            Id = 7,
                            SeatNumber = "B11"
                        },
                        new
                        {
                            Id = 8,
                            SeatNumber = "B12"
                        },
                        new
                        {
                            Id = 9,
                            SeatNumber = "B13"
                        },
                        new
                        {
                            Id = 10,
                            SeatNumber = "B14"
                        },
                        new
                        {
                            Id = 11,
                            SeatNumber = "C10"
                        },
                        new
                        {
                            Id = 12,
                            SeatNumber = "C11"
                        },
                        new
                        {
                            Id = 13,
                            SeatNumber = "C12"
                        },
                        new
                        {
                            Id = 14,
                            SeatNumber = "C13"
                        },
                        new
                        {
                            Id = 15,
                            SeatNumber = "B14"
                        });
                });

            modelBuilder.Entity("CinemaService.DataLayer.Models.BookingDTO", b =>
                {
                    b.HasOne("CinemaService.DataLayer.Models.CinemaShowDTO", "CinemaShow")
                        .WithMany()
                        .HasForeignKey("CinemaShowId");

                    b.HasOne("CinemaService.DataLayer.Models.SeatDTO", "Seat")
                        .WithMany()
                        .HasForeignKey("SeatId");

                    b.Navigation("CinemaShow");

                    b.Navigation("Seat");
                });
#pragma warning restore 612, 618
        }
    }
}
