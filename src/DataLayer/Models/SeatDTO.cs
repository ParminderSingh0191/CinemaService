using System.ComponentModel.DataAnnotations;

namespace CinemaService.DataLayer.Models
{
    public class SeatDTO
    {
        [Key]
        public int Id { get; set; }

        public string SeatNumber { get; set; }
    }
}
