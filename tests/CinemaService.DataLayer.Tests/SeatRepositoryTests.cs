using AutoFixture;
using CinemaService.DataLayer.Repositories;
using FluentAssertions;
using System;
using Xunit;

namespace CinemaService.DataLayer.Tests
{
    public class SeatRepositoryTests
    {
        private readonly SeatRepository _seatRepository;
        private readonly Fixture _fixture;

        public SeatRepositoryTests()
        {
            _fixture = new Fixture();
            _seatRepository = (SeatRepository)RepositorySetup.GetInMemorySeatRespository(Guid.NewGuid().ToString(), _fixture);
        }

        [Fact]
        public void GivenSeatEntities_WhenQueryForAvailableSeats_ShouldGetSeats()
        {
            // ACT
            var results = _seatRepository.GetAvailableSeats(_fixture.Create<int>());

            // ASSERT
            results.Should().NotBeNull();
            foreach (var res in results)
            {
                res.SeatNumber.Should().NotBeNull();
            }
        }

        [Fact]
        public void GivenSeatEntities_WhenQueryForSpecific_ShouldGetSeat()
        {
            // ARRANGE
            string seatNumber = "ABCD";

            // ACT
            var seat = _seatRepository.GetSeat(seatNumber);

            // ASSERT
            seat.Should().NotBeNull();
            seat.SeatNumber.Should().BeEquivalentTo(seatNumber);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void GivenInvalidSeatNumber_WhenQuerying_ShouldThrowArgumentException(string seatNumber)
        {
            Assert.Throws<ArgumentException>(() => _seatRepository.GetSeat(seatNumber));
        }

        [Fact]
        public void GivenCinemaShowIsBooked_WhenQueryForAvailableSeats_ShouldNotGetBookedSeat()
        {
            // ACT
            var results = _seatRepository.GetAvailableSeats(1);

            // ASSERT
            results.Should().NotBeNull();
            foreach (var res in results)
            {
                res.SeatNumber.Should().NotBeEquivalentTo("A10");
            }
        }
    }
}
