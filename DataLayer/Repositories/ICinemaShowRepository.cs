using CinemaService.DataLayer.Models;
using System.Collections.Generic;

namespace CinemaService.DataLayer.Repositories
{
    public interface ICinemaShowRepository
    {
        IEnumerable<CinemaShowDTO> GetAvailableCinemaShows();

        CinemaShowDTO GetCinemaShow(string showName);
    }
}
