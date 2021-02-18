using AutoFixture;
using CinemaService.Web.Api;
using CinemaService.Web.Api.Library.Models;
using CinemaService.Web.Api.Library.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CinemaService.IntegrationTests
{
    public class CinemaServiceIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {

        private readonly HttpClient _client;

        private readonly Fixture _fixture;

        private readonly Mock<ICinemaShowService> _cinemaShowServiceMock;

        private readonly Mock<ISeatService> _seatServiceMock;

        public CinemaServiceIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("https://localhost")
            });

            _fixture = new Fixture();

            _cinemaShowServiceMock = new Mock<ICinemaShowService>();
            _seatServiceMock = new Mock<ISeatService>();
        }

        [Fact]
        public async Task GivenApiWithHealthCheck_WhenStatusEndpointIsCalled_ShouldReturnHealthy()
        {
            // ARRANGE
            var url = "/status";

            // ACT
            HttpResponseMessage response = await _client.GetAsync(url);
            string stringResponse = await response.Content.ReadAsStringAsync();

            // ASSERT
            stringResponse.Should().BeEquivalentTo("Healthy");
        }

        [Fact]
        public async Task GivenListOfAvailableShows_WhenCallGetAvailableCinemaShows_ShouldReturnOK()
        {
            // ARRANGE
            var url = "/api/v1/cinema/GetAvailableCinemaShows";
            _cinemaShowServiceMock.Setup(cs => cs.GetAvailableCinemaShows()).Returns(_fixture.Create<IEnumerable<CinemaShow>>());

            // ACT
            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            var shows = JsonConvert.DeserializeObject<IEnumerable<CinemaShow>>(stringResponse);

            // ASSERT
            shows.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenListOfAvailableSeats_WhenCallGetAvailabelSeats_ShouldReturnOK()
        {
            // ARRANGE
            var show = _fixture.Create<CinemaShow>();
            var url = $"/api/v1/cinema/GetAvailabelSeats/{show.Name}";
            _seatServiceMock.Setup(s => s.GetAvailableSeats(show.Name)).Returns(_fixture.Create<IEnumerable<Seat>>());

            // ACT
            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            var seats = JsonConvert.DeserializeObject<IEnumerable<Seat>>(stringResponse);

            // ASSERT
            seats.Should().NotBeNull();
        }

        //private HttpClient CreateHttpClient()
        //{
        //    return _factory.SetupForTests(null,
        //                                   testServices =>
        //                                   {
        //                                       testServices.AddSingleton(_seatServiceMock.Object);
        //                                       testServices.AddSingleton(_cinemaShowServiceMock.Object);
        //                                   })
        //                    .CreateHttpClientForTest();
        //}
    }
}


// ARRANGE


// ACT


// ASSERT