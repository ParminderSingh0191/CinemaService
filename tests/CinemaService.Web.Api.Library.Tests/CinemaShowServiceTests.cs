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
    public class CinemaShowServiceTests
    {
        private readonly Fixture _fixture;

        private readonly Mock<ICinemaShowRepository> _cinemaShowRepositoryMock;

        private readonly Mock<IMapper> _mapperMock;

        public CinemaShowServiceTests()
        {
            _fixture = new Fixture();
            _cinemaShowRepositoryMock = new Mock<ICinemaShowRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public void GivenCinemaShows_WhenGetAvailableCinemaShows_ShouldReturnCinemaShows()
        {
            // ARRANGE
            var cinemaShows = _fixture.Create<IEnumerable<CinemaShowDTO>>();
            _cinemaShowRepositoryMock.Setup(r => r.GetAvailableCinemaShows()).Returns(cinemaShows);

            // ACT
            var cinemaService = new CinemaShowService(_cinemaShowRepositoryMock.Object, _mapperMock.Object);
            var response = cinemaService.GetAvailableCinemaShows();

            // ASSERT
            response.Should().NotBeNull();
        }

        [Fact]
        public void GivenNoCinemaShows_WhenGetAvailableCinemaShows_ShouldReturnNull()
        {
            // ARRANGE
            _cinemaShowRepositoryMock.Setup(r => r.GetAvailableCinemaShows()).Returns(() => null);

            // ACT
            var cinemaService = new CinemaShowService(_cinemaShowRepositoryMock.Object, _mapperMock.Object);
            var response = cinemaService.GetAvailableCinemaShows();

            // ASSERT
            response.Should().BeNull();
        }
    }
}
