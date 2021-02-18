using CinemaService.Web.Api.Library.Models;
using System.Collections.Generic;

namespace CinemaService.Web.Api.Library.Services
{
    public interface ICinemaShowService
    {
        IEnumerable<CinemaShow> GetAvailableCinemaShows();
    }
}
