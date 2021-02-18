using AutoMapper;
using CinemaService.DataLayer.Repositories;
using CinemaService.Web.Api.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaService.Web.Api.Library.Services
{
    public class CinemaShowService : ICinemaShowService
    {
        private readonly ICinemaShowRepository _cinemaShowRepository;
        private readonly IMapper _mapper;

        public CinemaShowService(ICinemaShowRepository cinemaShowRepository, IMapper mapper)
        {
            _cinemaShowRepository = cinemaShowRepository;
            _mapper = mapper;
        }

        public IEnumerable<CinemaShow> GetAvailableCinemaShows()
        {
            var shows = _cinemaShowRepository.GetAvailableCinemaShows();

            if(shows != null)
            {
                return (from show in shows
                        let res = _mapper.Map<CinemaShow>(show)
                        select res).ToList();
            }

            return null;
        }
    }
}
