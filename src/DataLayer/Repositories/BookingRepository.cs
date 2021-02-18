using CinemaService.DataLayer.Models;
using System;
using System.Linq;

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
            if (cinemaShow == null)
            {
                throw new ArgumentNullException(nameof(cinemaShow));
            }

            if (seat == null)
            {
                throw new ArgumentNullException(nameof(seat));
            }

            var booking = new BookingDTO
            {
                CinemaShow = cinemaShow,
                Seat = seat,
                IsBooked = true
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public BookingDTO GetBooking(int cinemaShowId, int seatId)
        {
            return _context.Bookings.Where(b => b.CinemaShow.Id == cinemaShowId && b.Seat.Id == seatId).FirstOrDefault();
        }
    }
}
