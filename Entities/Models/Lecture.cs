using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Lecture
    {
        [Key]
        public string Code { get; set; } = null!;

        public string? Name { get; set; }

        public int DepartmentId { get; set; }

        public required Department Department { get; set; }
        public required ICollection<LectureSession> LectureSessions { get; set; }

        public required ICollection<Exam> Exams { get; set; }

        public required ICollection<Enrollment> Enrollments { get; set; }
    }
}