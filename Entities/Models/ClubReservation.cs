using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("ClubReservation")]
    public class ClubReservation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Club")]
        public int ClubId { get; set; }
        public Club Club { get; set; }

        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public string EventRegisterLink { get; set; }
        public string EventName { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

        public string Status { get; set; }

        public string Banner { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

    }
}
