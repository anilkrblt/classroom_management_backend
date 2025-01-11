using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("Exam")]
    public class Exam
    {
        [Key]
        public int ExamId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }

        [Required]
        public string LectureCode { get; set; }

        [ForeignKey("LectureCode")]
        public Lecture Lecture { get; set; }

        public ICollection<ExamSession> ExamSessions { get; set; }
    }

}