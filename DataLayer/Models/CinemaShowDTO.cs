using System.ComponentModel.DataAnnotations;

namespace CinemaService.DataLayer.Models
{
    public class CinemaShowDTO
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        public bool IsAvailable { get; set; }
    }
}
