namespace CinemaService.Web.Api.Library.Services
{
    public interface IBookingService
    {
        bool BookSeat(string showName, string seatNumber);
    }
}
