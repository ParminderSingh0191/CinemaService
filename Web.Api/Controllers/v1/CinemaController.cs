using CinemaService.Web.Api.Library.Models;
using CinemaService.Web.Api.Library.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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

            if(shows == null)
            {
                return NotFound(null);
            }

            return Ok(shows);
        }

        [Route("[action]/{showName}")]
        [HttpGet]
        public ActionResult<IEnumerable<Seat>> GetAvailabelSeats([FromRoute] string showName)
        {
            if(string.IsNullOrWhiteSpace(showName))
            {
                return BadRequest("Cinema show name is not provided");
            }

            try
            {
                var seats = _seatService.GetAvailableSeats(showName);

                if (seats == null)
                {
                    return NotFound("Unable to find any available seats.");
                }

                return Ok(seats);
            }
            catch (Exception)
            {
                return Conflict("Unable to  complete the request");
                throw;
            }
        }

        [HttpPost("booking/{showName}/{seatNumber}")]
        public ActionResult BookCinemaShow([FromRoute] string showName, [FromRoute] string seatNumber)
        {
            if(string.IsNullOrWhiteSpace(showName) || string.IsNullOrWhiteSpace(seatNumber))
            {
                return BadRequest("Unable to process the request because of invalid parameter(s)");
            }

            try
            {
                var result = _bookingService.BookSeat(showName, seatNumber);

                if(result)
                {
                    return Ok("Hurrayyyy! Booking successful");
                }

                return BadRequest("Unable to book the show.");
            }
            catch (Exception)
            {
                return Conflict("Unable to complete the request");
                throw;
            }
        }
    }
}
