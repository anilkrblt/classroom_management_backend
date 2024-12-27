using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("Lecture")]
    public class Lecture
    {
        [Key]
        [Required, StringLength(20)]
        public string Code { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public int Grade { get; set; }

        public string Term { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
        public ICollection<LectureSession> LectureSessions { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<InstructorPreference> InstructorPreferences { get; set; }
        public ICollection<LectureReservation> LectureReservations { get; set; }

    }
}