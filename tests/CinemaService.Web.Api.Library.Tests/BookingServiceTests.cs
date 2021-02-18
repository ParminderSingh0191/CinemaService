using AutoFixture;
using CinemaService.DataLayer.Models;
using CinemaService.DataLayer.Repositories;
using CinemaService.Web.Api.Library.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CinemaService.Web.Api.Library.Tests
{
    public class BookingServiceTests
    {
        private readonly Fixture _fixture;

        private readonly Mock<ISeatRepository> _seatRepositoryMock;

        private readonly Mock<ICinemaShowRepository> _cinemaShowRepositoryMock;

        private readonly Mock<IBookingRepository> _bookingRepositoryMock;

        public BookingServiceTests()
        {
            _fixture = new Fixture();
            _cinemaShowRepositoryMock = new Mock<ICinemaShowRepository>();
            _seatRepositoryMock = new Mock<ISeatRepository>();
            _bookingRepositoryMock = new Mock<IBookingRepository>();
        }

        [Fact]
        public void GivenShowIsNotAvailable_WhenBookSeat_ShouldReturnFalse()
        {
            // ARRANGE
            var cinemaShow = _fixture.Build<CinemaShowDTO>().
                                    With(cs => cs.IsAvailable, false).
                                    Create();

            _cinemaShowRepositoryMock.Setup(c => c.GetCinemaShow(It.IsAny<string>())).Returns(cinemaShow);
            var bookingService = new BookingService(_cinemaShowRepositoryMock.Object, _seatRepositoryMock.Object, _bookingRepositoryMock.Object);

            // ACT
            var result = bookingService.BookSeat(cinemaShow.Name, It.IsAny<string>());

            // ASSERT
            result.Should().BeFalse();
        }

        [Fact]
        public void GivenShowHasNoSeats_WhenBookSeat_ShouldReturnFalse()
        {
            // ARRANGE
            var cinemaShow = _fixture.Build<CinemaShowDTO>().
                                    With(cs => cs.IsAvailable, true).
                                    Create();

            _cinemaShowRepositoryMock.Setup(c => c.GetCinemaShow(It.IsAny<string>())).Returns(cinemaShow);
            _seatRepositoryMock.Setup(c => c.GetAvailableSeats(cinemaShow.Id)).Returns(() => new List<SeatDTO>());
            var bookingService = new BookingService(_cinemaShowRepositoryMock.Object, _seatRepositoryMock.Object, _bookingRepositoryMock.Object);

            // ACT
            var result = bookingService.BookSeat(cinemaShow.Name, It.IsAny<string>());

            // ASSERT
            result.Should().BeFalse();
        }

        [Fact]
        public void GivenShowHasSeats_WhenBookSeat_ShouldReturnTrue()
        {
            // ARRANGE
            var cinemaShow = _fixture.Build<CinemaShowDTO>().
                                    With(cs => cs.IsAvailable, true).
                                    Create();
            var seats = _fixture.Create<IEnumerable<SeatDTO>>();

            _cinemaShowRepositoryMock.Setup(c => c.GetCinemaShow(It.IsAny<string>())).Returns(cinemaShow);
            _seatRepositoryMock.Setup(c => c.GetAvailableSeats(cinemaShow.Id)).Returns(seats);
            var bookingService = new BookingService(_cinemaShowRepositoryMock.Object, _seatRepositoryMock.Object, _bookingRepositoryMock.Object);

            // ACT
            var result = bookingService.BookSeat(cinemaShow.Name, It.IsAny<string>());

            // ASSERT
            result.Should().BeTrue();
        }
    }
}
