using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("InstructorPreference")]
    public class InstructorPreference
    {
        [Key]
        public int PreferenceId { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }

        [ForeignKey("Lecture")]
        public string LectureCode { get; set; }

        public string PreferredTime { get; set; } // JSON format for flexible time preferences

        public string UnavailableTimes { get; set; } // JSON format for unavailable times

        public Instructor Instructor { get; set; }
        public Lecture Lecture { get; set; }
    }
}