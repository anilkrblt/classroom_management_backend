using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace  Entities.Models
{
    public class ExamClass
    {
        public int ExamClassId { get; set; }

        public int ExamId { get; set; }
        public required Exam Exam { get; set; }

        public int RoomId { get; set; }
        [ForeignKey(nameof(RoomId))]
        public required Room Room { get; set; }
    }
}