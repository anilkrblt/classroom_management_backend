using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
        public DateTime ExamDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Code { get; set; }

        [ForeignKey("Code")]
        public Lecture Lecture { get; set; } = null!;


        public required ICollection<ExamClass> ExamClasses { get; set; }
    }

}