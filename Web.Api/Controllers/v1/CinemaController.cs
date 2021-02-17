using CinemaService.DataLayer.Repositories;
using CinemaService.Web.Api.Library.Models;
using CinemaService.Web.Api.Library.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaService.Web.Api.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ICinemaShowService _cinemaShowService;
        private readonly ISeatService _seatService;

        public CinemaController(IBookingService bookingService,
                                ICinemaShowService cinemaShowService,
                                ISeatService seatService)
        {
            _bookingService = bookingService;
            _cinemaShowService = cinemaShowService;
            _seatService = seatService;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult<IEnumerable<CinemaShow>> GetAvailableCinemaShows()
        {
            var shows = _cinemaShowService.GetAvailableCinemaShows();

            return Ok(shows);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult<IEnumerable<Seat>> GetAvailabelSeats()
        {
            var seats = _seatService.GetAvailableSeats();

            return Ok(seats);
        }

        [HttpPost("booking/{showName}/{seatNumber}")]
        public void BookCinemaShow([FromRoute] string showName, [FromRoute] string seatNumber)
        {
            _bookingService.BookSeat(showName, seatNumber);
        }
    }
}
