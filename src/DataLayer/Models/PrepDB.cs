using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CinemaService.DataLayer.Models
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<ApplicationDbContext>());
            }
        }

        public static void SeedData(ApplicationDbContext applicationDbContext)
        {
            Console.WriteLine("Starting migrate........");
            // This will apply migrations at run time
            // Not an ideal case but for demo purposes made it available here
            // Can be removed if we use pipeline to run migrations against the database
            if (applicationDbContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                applicationDbContext.Database.Migrate();
            }

            Console.WriteLine("Finished migrate......");

            Console.WriteLine("Starting the seed........");

            if (!applicationDbContext.CinemaShows.Any())
            {
                Console.WriteLine("Starting the add cinema show entities........");
                applicationDbContext.CinemaShows.AddRange(
                  new CinemaShowDTO { Id = 1, Name = "The White Tiger", IsAvailable = true },
                  new CinemaShowDTO { Id = 2, Name = "Spider man home coming", IsAvailable = true },
                  new CinemaShowDTO { Id = 3, Name = "Avengers", IsAvailable = true },
                  new CinemaShowDTO { Id = 4, Name = "Avengers Age of Ultron", IsAvailable = true },
                  new CinemaShowDTO { Id = 5, Name = "Root", IsAvailable = false }
                );
                Console.WriteLine("Finished adding cinema show entities........");
            }

            if (!applicationDbContext.Seats.Any())
            {
                Console.WriteLine("Starting the add seat entities........");
                applicationDbContext.Seats.AddRange(
                    new SeatDTO { Id = 1, SeatNumber = "A10" },
                    new SeatDTO { Id = 2, SeatNumber = "A11" },
                    new SeatDTO { Id = 3, SeatNumber = "A12" },
                    new SeatDTO { Id = 4, SeatNumber = "A13" },
                    new SeatDTO { Id = 5, SeatNumber = "A14" },
                    new SeatDTO { Id = 6, SeatNumber = "B10" },
                    new SeatDTO { Id = 7, SeatNumber = "B11" },
                    new SeatDTO { Id = 8, SeatNumber = "B12" },
                    new SeatDTO { Id = 9, SeatNumber = "B13" },
                    new SeatDTO { Id = 10, SeatNumber = "B14" },
                    new SeatDTO { Id = 11, SeatNumber = "C10" },
                    new SeatDTO { Id = 12, SeatNumber = "C11" },
                    new SeatDTO { Id = 13, SeatNumber = "C12" },
                    new SeatDTO { Id = 14, SeatNumber = "C13" },
                    new SeatDTO { Id = 15, SeatNumber = "B14" }
                );
                Console.WriteLine("Finished adding seat entities........");
            }

            applicationDbContext.SaveChanges();

            Console.WriteLine("Finished the seed........");
        }
    }
}