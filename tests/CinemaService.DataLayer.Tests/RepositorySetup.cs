using AutoFixture;
using CinemaService.DataLayer.Models;
using CinemaService.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaService.DataLayer.Tests
{
    public static class RepositorySetup
    {
        public static ICinemaShowRepository GetInMemoryCinemaShowRespository(string dbName, Fixture fixture)
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName)
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var cinemaShow = fixture.Create<CinemaShowDTO>();
            var cinemShowWithSpecificName = fixture.Build<CinemaShowDTO>().
                                            With(cs => cs.Name, "IronMan").
                                            Create();

            var toUpdateCinemaShow = fixture.Build<CinemaShowDTO>().
                                            With(cs => cs.Name, "Avengers").
                                            With(cs => cs.IsAvailable, true).
                                            Create();

            var cinemaShowWhichIsNotAvailable = fixture.Build<CinemaShowDTO>().
                                            With(cs => cs.IsAvailable, false).
                                            Create();



            dbContext.CinemaShows.Add(cinemaShow);
            dbContext.CinemaShows.Add(cinemShowWithSpecificName);
            dbContext.CinemaShows.Add(toUpdateCinemaShow);
            dbContext.SaveChanges();
            return new CinemaShowRepository(dbContext);
        }

        public static ISeatRepository GetInMemorySeatRespository(string dbName, Fixture fixture)
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName)
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var cinemaShow = fixture.Build<CinemaShowDTO>().
                                    With(cs => cs.Id, 1).
                                    Create();

            var seat = fixture.Build<SeatDTO>().
                               With(s => s.Id, 1).
                               With(s => s.SeatNumber, "A10").
                               Create();
            var specificSeat = fixture.Build<SeatDTO>().
                                            With(s => s.SeatNumber, "ABCD").
                                            Create();

            var booking = fixture.Build<BookingDTO>().
                                 With(b => b.CinemaShow, cinemaShow).
                                 With(b => b.Seat, seat).
                                 With(b => b.IsBooked, true).
                                 Create();

            dbContext.CinemaShows.Add(cinemaShow);

            dbContext.Seats.Add(seat);
            dbContext.Seats.Add(specificSeat);

            dbContext.Bookings.Add(booking);

            dbContext.SaveChanges();

            return new SeatRepository(dbContext);
        }

        public static IBookingRepository GetInMemoryBookingRespository(string dbName, Fixture fixture)
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>().
                                                             UseInMemoryDatabase(dbName)
                                                             .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            return new BookingRepository(dbContext);
        }
    }
}
