using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class ClubReservation
    {

        [Key]
        public int ReservationId { get; set; }
        [ForeignKey(nameof(ReservationId))]
        public Reservation Reservation { get; set; } = null!;

        public int ClubId { get; set; }

        public required Club Club { get; set; }
    }
}