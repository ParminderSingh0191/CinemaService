using AutoFixture;
using AutoMapper;
using CinemaService.DataLayer.Models;
using CinemaService.DataLayer.Repositories;
using CinemaService.Web.Api.Library.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CinemaService.Web.Api.Library.Tests
{
    public class SeatServiceTests
    {
        private readonly Fixture _fixture;

        private readonly Mock<ICinemaShowRepository> _cinemaShowRepositoryMock;

        private readonly Mock<ISeatRepository> _seatRepositoryMock;

        private readonly Mock<IMapper> _mapperMock;

        public SeatServiceTests()
        {
            _fixture = new Fixture();
            _cinemaShowRepositoryMock = new Mock<ICinemaShowRepository>();
            _seatRepositoryMock = new Mock<ISeatRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public void GivenCinemaShowAndSeats_WhenGetAvailableSeats_ShouldReturnSeats()
        {
            // ARRANGE
            var cinemaShow = _fixture.Create<CinemaShowDTO>();
            _cinemaShowRepositoryMock.Setup(r => r.GetCinemaShow(It.IsAny<string>())).Returns(cinemaShow);

            var seats = _fixture.Create<IEnumerable<SeatDTO>>();
            _seatRepositoryMock.Setup(s => s.GetAvailableSeats(cinemaShow.Id)).Returns(seats);

            // ACT
            var seatService = new SeatService(_seatRepositoryMock.Object, _cinemaShowRepositoryMock.Object, _mapperMock.Object);
            var response = seatService.GetAvailableSeats(cinemaShow.Name);

            // ASSERT
            response.Should().NotBeNull();
        }

        [Fact]
        public void GivenCinemaShowWithoutAvailableSeats_WhenGetAvailableSeats_ShouldReturnNull()
        {
            // ARRANGE
            var cinemaShow = _fixture.Create<CinemaShowDTO>();
            _cinemaShowRepositoryMock.Setup(r => r.GetCinemaShow(It.IsAny<string>())).Returns(cinemaShow);

            _seatRepositoryMock.Setup(s => s.GetAvailableSeats(cinemaShow.Id)).Returns(() => null);

            // ACT
            var seatService = new SeatService(_seatRepositoryMock.Object, _cinemaShowRepositoryMock.Object, _mapperMock.Object);
            var response = seatService.GetAvailableSeats(cinemaShow.Name);

            // ASSERT
            response.Should().BeNull();
        }
    }
}
