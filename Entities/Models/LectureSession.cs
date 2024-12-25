using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("LectureSession")]
    public class LectureSession
    {
        [Key]
        public int LectureSessionId { get; set; }

        [Required, StringLength(20)]
        public string? DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }
        // ek ders mi değil mi

        [Required]
        public TimeSpan EndTime { get; set; }

        [ForeignKey("Lecture")]
        public string? LectureCode { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }

        public Lecture Lecture { get; set; }
        public Room Room { get; set; }
        public Instructor Instructor { get; set; }
    }


}