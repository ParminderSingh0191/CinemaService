using CinemaService.DataLayer.Models;

namespace CinemaService.DataLayer.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void BookShow(CinemaShowDTO cinemaShow, SeatDTO seat)
        {
            var booking = new BookingDTO
            {
                CinemaShow = cinemaShow,
                Seat = seat,
                IsBooked = true
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }
    }
}
