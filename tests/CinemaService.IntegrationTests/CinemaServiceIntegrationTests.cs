using CinemaService.Web.Api;
using CinemaService.Web.Api.Library.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CinemaService.IntegrationTests
{
    public class CinemaServiceIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {

        private readonly HttpClient _client;

        public CinemaServiceIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("https://localhost")
            });
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

            // ACT
            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            var shows = JsonConvert.DeserializeObject<IEnumerable<CinemaShow>>(stringResponse);

            // ASSERT
            shows.Should().NotBeNull();
            shows.Should().HaveCount(2);
        }

        [Fact]
        public async Task GivenListOfAvailableSeats_WhenCallGetAvailabelSeats_ShouldReturnOK()
        {
            // ARRANGE
            var url = "/api/v1/cinema/GetAvailabelSeats/Avengers Age Of Ultron";

            // ACT
            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            var seats = JsonConvert.DeserializeObject<IEnumerable<Seat>>(stringResponse);

            // ASSERT
            seats.Should().NotBeNull();
            seats.Should().HaveCount(3);
        }

        [Fact]
        public async Task GivenListOfAvailableSeatsAndShows_WhenCallingBookShow_ShouldReturnOK()
        {
            // ARRANGE
            var bookingURl = "/api/v1/cinema/booking/Avengers Age Of Ultron/A10";
            var getSeatsUrl = "/api/v1/cinema/GetAvailabelSeats/Avengers Age Of Ultron";

            // ACT
            HttpResponseMessage response = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, bookingURl));
            response.EnsureSuccessStatusCode();

            HttpResponseMessage seatsResponse = await _client.GetAsync(getSeatsUrl);
            seatsResponse.EnsureSuccessStatusCode();
            string stringResponse = await seatsResponse.Content.ReadAsStringAsync();
            var seats = JsonConvert.DeserializeObject<IEnumerable<Seat>>(stringResponse);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            seats.Should().NotBeNull();
            seats.Should().HaveCount(2);
        }
    }
}