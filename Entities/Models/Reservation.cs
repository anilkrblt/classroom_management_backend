using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [ForeignKey("Instructor")]
        public int ApprovedBy { get; set; }
        public Instructor Instructor { get; set; }

        public string? Description { get; set; }

        public required ICollection<LectureReservation> LectureReservations { get; set; }
        public required ICollection<ClubReservation> ClubReservations { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }




    }
}