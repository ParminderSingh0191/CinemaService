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
    }
}
