using AutoFixture;
using CinemaService.DataLayer;
using CinemaService.DataLayer.Models;

namespace CinemaService.IntegrationTests
{
    public static class SeedData
    {
        public static void PopulateWithTestData(ApplicationDbContext context)
        {
            var fixture = new Fixture();

            // CinemaShow seed
            var cinemaShow1 = fixture.Build<CinemaShowDTO>().
                              With(cs => cs.Id, 1).
                              With(cs => cs.IsAvailable, true).
                              Create();
            var cinemaShow2 = fixture.Build<CinemaShowDTO>().
                              With(cs => cs.Id, 2).
                              With(cs => cs.Name, "Avengers Age Of Ultron").
                              With(cs => cs.IsAvailable, true).
                              Create();
            var cinemaShow3 = fixture.Build<CinemaShowDTO>().
                              With(cs => cs.Id, 3).
                              With(cs => cs.IsAvailable, false).
                              Create();

            context.CinemaShows.AddRange(cinemaShow1, cinemaShow2, cinemaShow3);

            // Seats seed
            var seat1 = fixture.Build<SeatDTO>().
                        With(s => s.Id, 101).
                        With(s => s.SeatNumber, "A10").
                        Create();
            var seat2 = fixture.Build<SeatDTO>().
                        With(s => s.Id, 102).
                        Create();
            var seat3 = fixture.Build<SeatDTO>().
                        With(s => s.Id, 103).
                        Create();

            context.Seats.AddRange(seat1, seat2, seat3);

            context.SaveChanges();
        }
    }
}
