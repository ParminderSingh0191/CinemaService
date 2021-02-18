using CinemaService.DataLayer.Models;

namespace CinemaService.DataLayer.Repositories
{
    public interface IBookingRepository
    {
        void BookShow(CinemaShowDTO cinemaShow, SeatDTO seat);

        BookingDTO GetBooking(int cinemaShowId, int seatId);
    }
}
