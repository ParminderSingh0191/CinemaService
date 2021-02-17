using CinemaService.DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace CinemaService.DataLayer.Repositories
{
    public interface ISeatRepository
    {
        void BookSeat(SeatDTO seat);

        IEnumerable<SeatDTO> GetAvailableSeats();

        SeatDTO GetSeat(string seatNumber);
    }

    public class SeatRepository : ISeatRepository
    {
        private readonly ApplicationDbContext _context;

        public SeatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void BookSeat(SeatDTO seat)
        {
            seat.IsBooked = true;
            _context.SaveChanges();
        }

        public IEnumerable<SeatDTO> GetAvailableSeats()
        {
            return _context.Seats.Where(s => s.IsBooked == false);
        }

        public SeatDTO GetSeat(string seatNumber)
        {
            return _context.Seats.Where(s => s.SeatNumber == seatNumber).FirstOrDefault();
        }
    }
}
