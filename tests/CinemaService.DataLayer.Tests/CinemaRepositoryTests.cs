using AutoFixture;
using CinemaService.DataLayer.Repositories;
using System;
using Xunit;

namespace CinemaService.DataLayer.Tests
{
    public class CinemaRepositoryTests
    {
        private readonly CinemaShowRepository _cinemaShowRepository;
        private readonly Fixture _fixture;

        public CinemaRepositoryTests()
        {
            _fixture = new Fixture();
            _cinemaShowRepository = (CinemaShowRepository)RepositorySetup.GetInMemoryCinemaShowRespository(Guid.NewGuid().ToString(), _fixture);
        }

        [Fact]
        public void GivenCinemaShowEntities_WhenQueryForAvailableShows_ShouldGetCinemaShows()
        {
            // ACT
            var results = _cinemaShowRepository.GetAvailableCinemaShows();

            // ASSERT
            Assert.NotNull(results);

            foreach (var res in results)
            {
                Assert.True(res.IsAvailable);
            }
        }

        [Fact]
        public void GivenCinemaShowEntities_WhenQueryForSpecificShow_ShouldGetCinemaShow()
        {
            // ARRANGE
            string showName = "IronMan";

            // ACT
            var result = _cinemaShowRepository.GetCinemaShow(showName);

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal(showName, result.Name);
        }

        [Fact]
        public void GivenCinemaShowEntities_WhenUpdateSpecificShow_ShouldBeAbleToUpdateSuccessfully()
        {
            // ARRANGE
            string showName = "Avengers";
            var show = _cinemaShowRepository.GetCinemaShow(showName);

            // ACT
            show.IsAvailable = false;
            _cinemaShowRepository.UpdateCinemaShow(show);
            var updatedShow = _cinemaShowRepository.GetCinemaShow(showName);

            Assert.NotNull(updatedShow);
            Assert.False(updatedShow.IsAvailable);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void GivenInvalidCinemaShowName_WhenQuerying_ShouldThrowArgumentException(string showName)
        {
            Assert.Throws<ArgumentException>(() => _cinemaShowRepository.GetCinemaShow(showName));
        }

        [Fact]
        public void GivenInvalidCinemaShow_WhenUpdateCinemaShow_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _cinemaShowRepository.UpdateCinemaShow(null));
        }
    }
}
