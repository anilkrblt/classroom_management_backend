using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Entities.Models
{
    public enum ReservationType
    {
        Regular,
        ClubActivity,
        LecturePostponement
    }

    [Table("Reservation")]
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public ReservationType ReservationType { get; set; }

        [ForeignKey("Lecture")]
        public string LectureCode { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

      //  public string EventRegisterLink { get; set; }

        public string ClubName { get; set; }

       // public string EventName { get; set; }

       // public string Link { get; set; }

        public int CreatedBy { get; set; }

        public Lecture Lecture { get; set; }
        public Room Room { get; set; }
    }
}