using CinemaService.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaService.DataLayer.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly ApplicationDbContext _context;

        public SeatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SeatDTO> GetAvailableSeats(int showId)
        {
            List<SeatDTO> seats = new List<SeatDTO>();
            var bookings = _context.Bookings.
                            Include(s => s.Seat).
                            Include(cs => cs.CinemaShow).
                            Where(b => b.IsBooked).ToList();

            foreach (var seat in _context.Seats)
            {
                var booking = bookings.Where(b => b.Seat.Id == seat.Id && b.CinemaShow.Id == showId).FirstOrDefault();
                if (booking == null)
                {
                    seats.Add(seat);
                }
            }
            return seats;
        }

        public SeatDTO GetSeat(string seatNumber)
        {
            if(string.IsNullOrWhiteSpace(seatNumber))
            {
                throw new ArgumentException(nameof(seatNumber));
            }

            return _context.Seats.Where(s => s.SeatNumber == seatNumber).FirstOrDefault();
        }
    }
}
