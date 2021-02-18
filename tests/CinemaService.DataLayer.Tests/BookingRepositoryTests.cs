using AutoFixture;
using CinemaService.DataLayer.Models;
using CinemaService.DataLayer.Repositories;
using System;
using Xunit;

namespace CinemaService.DataLayer.Tests
{
    public class BookingRepositoryTests
    {
        private readonly BookingRepository _bookingRepository;
        private readonly Fixture _fixture;

        public BookingRepositoryTests()
        {
            _fixture = new Fixture();
            _bookingRepository = (BookingRepository)RepositorySetup.GetInMemoryBookingRespository(Guid.NewGuid().ToString(), _fixture);
        }

        [Fact]
        public void GivenCinemaShowAndSeatEntity_WhenCreateBooking_ShouldAddBookingSuccessfully()
        {
            // ARRANGE
            var cinemaShow = _fixture.Create<CinemaShowDTO>();
            var seat = _fixture.Create<SeatDTO>();

            // ACT
            _bookingRepository.BookShow(cinemaShow, seat);
            var booking = _bookingRepository.GetBooking(cinemaShow.Id, seat.Id);

            // ASSERT
            Assert.NotNull(booking);
            Assert.True(booking.IsBooked);
        }

        [Fact]
        public void GivenInvalidCinemaShow_WhenAddBooking_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _bookingRepository.BookShow(null, _fixture.Create<SeatDTO>()));
        }

        [Fact]
        public void GivenInvalidSeat_WhenAddBooking_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _bookingRepository.BookShow(_fixture.Create<CinemaShowDTO>(), null));
        }
    }
}
