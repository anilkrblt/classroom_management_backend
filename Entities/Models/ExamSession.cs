using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{

    [Table("ExamSession")]
    public class ExamSession
    {
        [Key]
        public int ExamSessionId { get; set; }

        [Required]
        public DateTime ExamDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        public Room Room { get; set; }

        [ForeignKey("Exam")]
        public int ExamId { get; set; }

        public Exam Exam { get; set; }
    }
}