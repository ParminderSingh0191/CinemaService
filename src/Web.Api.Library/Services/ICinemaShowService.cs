using CinemaService.Web.Api.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaService.Web.Api.Library.Services
{
    public interface ICinemaShowService
    {
        IEnumerable<CinemaShow> GetAvailableCinemaShows();
    }
}
