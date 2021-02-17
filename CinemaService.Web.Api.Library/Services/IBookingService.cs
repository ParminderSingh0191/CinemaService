using CinemaService.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaService.Web.Api.Library.Services
{
    public interface IBookingService
    {
        void BookSeat(string showName, string seatNumber);
    }

    public class BookingService : IBookingService
    {
        private readonly ICinemaShowRepository _cinemaShowRepository;
        private readonly ISeatRepository _seatRepository;

        public BookingService(ICinemaShowRepository cinemaShowRepository,
                              ISeatRepository seatRepository)
        {
            _cinemaShowRepository = cinemaShowRepository;
            _seatRepository = seatRepository;
        }

        public void BookSeat(string showName, string seatNumber)
        {
            var show = _cinemaShowRepository.GetCinemaShow(showName);
            var seat = _seatRepository.GetSeat(seatNumber);

            if(show.IsAvailable && !seat.IsBooked)
            {
                _seatRepository.BookSeat(seat);
            }

        }
    }
}
