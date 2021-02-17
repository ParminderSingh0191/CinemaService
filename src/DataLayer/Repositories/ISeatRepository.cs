using CinemaService.DataLayer.Models;
using System.Collections.Generic;

namespace CinemaService.DataLayer.Repositories
{
    public interface ISeatRepository
    {
        IEnumerable<SeatDTO> GetAvailableSeats(int showId);

        SeatDTO GetSeat(string seatNumber);
    }
}
