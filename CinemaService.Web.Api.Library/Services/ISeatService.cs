using AutoMapper;
using CinemaService.DataLayer.Repositories;
using CinemaService.Web.Api.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace CinemaService.Web.Api.Library.Services
{
    public interface ISeatService
    {
        IEnumerable<Seat> GetAvailableSeats();
    }

    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IMapper _mapper;

        public SeatService(ISeatRepository seatRepository, IMapper mapper)
        {
            _seatRepository = seatRepository;
            _mapper = mapper;
        }

        public IEnumerable<Seat> GetAvailableSeats()
        {
            var seats = _seatRepository.GetAvailableSeats();
            return (from seat in seats
                    let res = _mapper.Map<Seat>(seat)
                    select res).ToList();
        }
    }

}
