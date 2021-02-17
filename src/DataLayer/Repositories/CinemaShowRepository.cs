using CinemaService.DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace CinemaService.DataLayer.Repositories
{
    public class CinemaShowRepository : ICinemaShowRepository
    {
        private readonly ApplicationDbContext _context;

        public CinemaShowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CinemaShowDTO> GetAvailableCinemaShows()
        {
            return _context.CinemaShows.Where(cs => cs.IsAvailable);
        }

        public CinemaShowDTO GetCinemaShow(string showName)
        {
            return _context.CinemaShows.Where(cs => cs.Name == showName).FirstOrDefault();
        }

        public void UpdateCinemaShow(CinemaShowDTO cinemaShow)
        {
            _context.SaveChanges();
        }
    }
}
