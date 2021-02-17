using CinemaService.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CinemaService.DataLayer
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<CinemaShowDTO> CinemaShows { get; set; }

        public DbSet<SeatDTO> Seats { get; set; }

        public DbSet<BookingDTO> Bookings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
