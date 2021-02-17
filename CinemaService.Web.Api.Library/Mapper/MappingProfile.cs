using AutoMapper;
using CinemaService.DataLayer.Models;
using CinemaService.Web.Api.Library.Models;

namespace CinemaService.Web.Api.Library.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CinemaShowDTO, CinemaShow>();
            CreateMap<SeatDTO, Seat>();
        }
    }
}
