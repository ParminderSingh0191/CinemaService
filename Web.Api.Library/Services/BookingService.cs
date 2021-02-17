using CinemaService.DataLayer.Repositories;

namespace CinemaService.Web.Api.Library.Services
{
    public class BookingService : IBookingService
    {
        private readonly ICinemaShowRepository _cinemaShowRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IBookingRepository _bookingRepository;

        public BookingService(ICinemaShowRepository cinemaShowRepository,
                              ISeatRepository seatRepository,
                              IBookingRepository bookingRepository)
        {
            _cinemaShowRepository = cinemaShowRepository;
            _seatRepository = seatRepository;
            _bookingRepository = bookingRepository;
        }

        public void BookSeat(string showName, string seatNumber)
        {
            var show = _cinemaShowRepository.GetCinemaShow(showName);
            var seat = _seatRepository.GetSeat(seatNumber);

            _bookingRepository.BookShow(show, seat);
        }
    }
}
