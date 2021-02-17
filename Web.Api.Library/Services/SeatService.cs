using AutoMapper;
using CinemaService.DataLayer.Repositories;
using CinemaService.Web.Api.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace CinemaService.Web.Api.Library.Services
{
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;
        private readonly ICinemaShowRepository _cinemaShowRepository;
        private readonly IMapper _mapper;

        public SeatService(ISeatRepository seatRepository,
                           ICinemaShowRepository cinemaShowRepository,
                           IMapper mapper)
        {
            _seatRepository = seatRepository;
            _cinemaShowRepository = cinemaShowRepository;
            _mapper = mapper;
        }

        public IEnumerable<Seat> GetAvailableSeats(string showName)
        {
            var show = _cinemaShowRepository.GetCinemaShow(showName);
            var seats = _seatRepository.GetAvailableSeats(show.Id);

            return (from seat in seats
                    let res = _mapper.Map<Seat>(seat)
                    select res).ToList();
        }
    }

}
