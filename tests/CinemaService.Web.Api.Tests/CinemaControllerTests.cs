using AutoFixture;
using CinemaService.Web.Api.Controllers.v1;
using CinemaService.Web.Api.Library.Models;
using CinemaService.Web.Api.Library.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CinemaService.Web.Api.Tests
{
    public class CinemaControllerTests
    {
        private readonly Fixture _fixture;

        private readonly Mock<IBookingService> _bookingServiceMock;

        private readonly Mock<ICinemaShowService> _cinemaShowServiceMock;

        private readonly Mock<ISeatService> _seatServiceMock;

        public CinemaControllerTests()
        {
            _fixture = new Fixture();
            _bookingServiceMock = new Mock<IBookingService>();
            _cinemaShowServiceMock = new Mock<ICinemaShowService>();
            _seatServiceMock = new Mock<ISeatService>();
        }

        [Fact]
        public void GivenHaveCinemaShows_WhenCallGetActiveCinemaShows_ShouldGetOk()
        {
            // ARRANGE
            var shows = _fixture.Create<IEnumerable<CinemaShow>>();
            _cinemaShowServiceMock.Setup(cs => cs.GetAvailableCinemaShows()).Returns(shows);

            var controller = new CinemaController(_bookingServiceMock.Object, _cinemaShowServiceMock.Object, _seatServiceMock.Object);

            // ACT
            var response = controller.GetAvailableCinemaShows();
            var result = response.Result as ObjectResult;

            // ASSERT
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200); 
        }

        [Fact]
        public void GivenNoCinemaShows_WhenCallGetActiveCinemaShows_ShouldGetNotFound()
        {
            // ARRANGE
            _cinemaShowServiceMock.Setup(cs => cs.GetAvailableCinemaShows()).Returns(() => null);

            var controller = new CinemaController(_bookingServiceMock.Object, _cinemaShowServiceMock.Object, _seatServiceMock.Object);

            // ACT
            var response = controller.GetAvailableCinemaShows();
            var result = response.Result as ObjectResult;

            // ASSERT
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void GivenInvalidShowName_WhenCallingAction_ShouldGetBadRequest(string showName)
        {
            // ARRANGE
            var controller = new CinemaController(_bookingServiceMock.Object, _cinemaShowServiceMock.Object, _seatServiceMock.Object);

            // ACT
            var response = controller.GetAvailabelSeats(showName);
            var result = response.Result as ObjectResult;

            // ASSERT
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public void GivenNoAvailableSeats_WhenCallingAction_ShouldGetNotFound()
        {
            // ARRANGE
            _seatServiceMock.Setup(s => s.GetAvailableSeats(It.IsAny<string>())).Returns(()=> null);
            var controller = new CinemaController(_bookingServiceMock.Object, _cinemaShowServiceMock.Object, _seatServiceMock.Object);

            // ACT
            var response = controller.GetAvailabelSeats(_fixture.Create<string>());
            var result = response.Result as ObjectResult;

            // ASSERT
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public void GivenAvailableSeats_WhenCallingActionExceptionOccurs_ShouldGetConflict()
        {
            // ARRANGE
            _seatServiceMock.Setup(s => s.GetAvailableSeats(It.IsAny<string>())).Returns(()=> throw new Exception());
            var controller = new CinemaController(_bookingServiceMock.Object, _cinemaShowServiceMock.Object, _seatServiceMock.Object);

            // ACT
            var response = controller.GetAvailabelSeats(_fixture.Create<string>());
            var result = response.Result as ObjectResult;

            // ASSERT
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(409);
        }

        [Fact]
        public void GivenAvailableSeats_WhenCallingAction_ShouldGetOk()
        {
            // ARRANGE
            _seatServiceMock.Setup(s => s.GetAvailableSeats(It.IsAny<string>())).Returns(_fixture.Create<IEnumerable<Seat>>());
            var controller = new CinemaController(_bookingServiceMock.Object, _cinemaShowServiceMock.Object, _seatServiceMock.Object);

            // ACT
            var response = controller.GetAvailabelSeats(_fixture.Create<string>());
            var result = response.Result as ObjectResult;

            // ASSERT
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Theory]
        [InlineData(null, "validValue")]
        [InlineData("", "validValue")]
        [InlineData("   ", "validValue")]
        [InlineData("validValue", null)]
        [InlineData("validValue", "")]
        [InlineData("validValue", "   ")]
        public void GivenInvalidParameterName_WhenCallingAction_ShouldGetBadRequest(string showName, string seatNumber)
        {
            // ARRANGE
            var controller = new CinemaController(_bookingServiceMock.Object, _cinemaShowServiceMock.Object, _seatServiceMock.Object);

            // ACT
            var response = controller.BookCinemaShow(showName, seatNumber) as ObjectResult;

            // ASSERT
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(400);
        }

        [Fact]
        public void GivenBookingAShow_WhenCallingActionExceptionOccurs_ShouldGetConflict()
        {
            // ARRANGE
            _bookingServiceMock.Setup(s => s.BookSeat(It.IsAny<string>(), It.IsAny<string>())).Returns(() => throw new Exception());
            var controller = new CinemaController(_bookingServiceMock.Object, _cinemaShowServiceMock.Object, _seatServiceMock.Object);

            // ACT
            var response = controller.BookCinemaShow(_fixture.Create<string>(), _fixture.Create<string>())as ObjectResult;

            // ASSERT
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(409);
        }

        [Fact]
        public void GivenBookingAShow_WhenSuccessfullyBookAShow_ShouldGetOk()
        {
            // ARRANGE
            _bookingServiceMock.Setup(s => s.BookSeat(It.IsAny<string>(), It.IsAny<string>())).Returns(() => true);
            var controller = new CinemaController(_bookingServiceMock.Object, _cinemaShowServiceMock.Object, _seatServiceMock.Object);

            // ACT
            var response = controller.BookCinemaShow(_fixture.Create<string>(), _fixture.Create<string>()) as ObjectResult;

            // ASSERT
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GivenBookingAShow_WhenNotAbleToBookAShow_ShouldGetBadRequest()
        {
            // ARRANGE
            _bookingServiceMock.Setup(s => s.BookSeat(It.IsAny<string>(), It.IsAny<string>())).Returns(() => false);
            var controller = new CinemaController(_bookingServiceMock.Object, _cinemaShowServiceMock.Object, _seatServiceMock.Object);

            // ACT
            var response = controller.BookCinemaShow(_fixture.Create<string>(), _fixture.Create<string>()) as ObjectResult;

            // ASSERT
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(400);
        }
    }
}
