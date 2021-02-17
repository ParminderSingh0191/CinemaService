using System.ComponentModel.DataAnnotations;

namespace CinemaService.DataLayer.Models
{
    public class BookingDTO
    {
        [Key]
        public int Id { get; set; }

        public CinemaShowDTO CinemaShow { get; set; }

        public SeatDTO Seat { get; set; }
    }
}
