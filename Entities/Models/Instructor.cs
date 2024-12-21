using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Title { get; set; }



        public int DepartmentId { get; set; }

        public required Department Department { get; set; }

        public required ICollection<LectureSession> LectureSessions { get; set; }

        public required ICollection<LectureReservation> LectureReservations { get; set; }




    }
}