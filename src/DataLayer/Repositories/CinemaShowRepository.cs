using CinemaService.DataLayer.Models;
using System;
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
            if(string.IsNullOrWhiteSpace(showName))
            {
                throw new ArgumentException(nameof(showName));
            }

            return _context.CinemaShows.Where(cs => cs.Name == showName).FirstOrDefault();
        }

        public void UpdateCinemaShow(CinemaShowDTO cinemaShow)
        {
            if (cinemaShow == null)
            {
                throw new ArgumentNullException(nameof(cinemaShow));
            }

            _context.SaveChanges();
        }
    }
}
