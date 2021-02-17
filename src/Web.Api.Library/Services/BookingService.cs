using CinemaService.DataLayer.Repositories;
using System;
using System.Linq;

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

        // Check if we have show whose status is available and seats are available
        // Then complete the booking
        // Otherwise if there are no seats available then update the status of the show to unavailable and return false
        // If the show is unavail
        public bool BookSeat(string showName, string seatNumber)
        {
            try
            {
                var show = _cinemaShowRepository.GetCinemaShow(showName);

                if (!show.IsAvailable)
                {
                    return false;
                }

                var seats = _seatRepository.GetAvailableSeats(show.Id).ToList();
                if (seats.Count >= 0)
                {
                    _bookingRepository.BookShow(show, seats.Where(s => s.SeatNumber == seatNumber).FirstOrDefault());

                    return true;
                }

                show.IsAvailable = false;
                _cinemaShowRepository.UpdateCinemaShow(show);

                return false;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
