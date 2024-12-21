using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class LectureSession
    {
        public int LectureSessionId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }


        public string LectureCode { get; set; } = null!;

        public required Lecture Lecture { get; set; }


        public int InstructorId { get; set; }

        public required Instructor Instructor { get; set; }
        

        [ForeignKey("RoomId")]
        public Room Room { get; set; }
        public int RoomId {  get; set; }


    }
}