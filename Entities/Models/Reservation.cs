using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Entities.Models
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime EventDate { get; set; }
        
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public ICollection<ClubReservation> ClubReservations { get; set; }
        public ICollection<LectureReservation> LectureReservations { get; set; }



    }
}



