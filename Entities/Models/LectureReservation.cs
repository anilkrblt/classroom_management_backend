using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("LectureReservation")]
    public class LectureReservation
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Lecture")]
        public string Code { get; set; }
        public Lecture Lecture { get; set; }
        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }

        public Instructor Instructor { get; set; }
    }
}