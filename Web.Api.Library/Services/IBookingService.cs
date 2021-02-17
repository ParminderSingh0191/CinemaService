namespace CinemaService.Web.Api.Library.Services
{
    public interface IBookingService
    {
        void BookSeat(string showName, string seatNumber);
    }
}
