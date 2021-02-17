using CinemaService.Web.Api.Library.Models;
using System.Collections.Generic;

namespace CinemaService.Web.Api.Library.Services
{
    public interface ISeatService
    {
        IEnumerable<Seat> GetAvailableSeats(string showName);
    }

}
